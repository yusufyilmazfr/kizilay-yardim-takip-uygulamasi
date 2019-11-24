using Kizilay.Core.CrossCuttingConcerns.Cache;
using Ninject;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Core.Aspects.Ninject.Cache
{
    public class CacheInterceptor : IInterceptor
    {
        private int _cacheDuration;
        private ICache _cache { get; set; }

        public int CacheDuration
        {
            get
            {
                return _cacheDuration;
            }
            set
            {
                if (value > 0)
                {
                    _cacheDuration = value;
                }
            }
        }

        public CacheInterceptor(ICache cache)
        {
            _cache = cache;
        }

        public void Intercept(IInvocation invocation)
        {
            string key = String.Format("{0}.{1}.{2}",
            invocation.Request.Method.ReflectedType.Namespace,
            invocation.Request.Method.ReflectedType.Name,
            invocation.Request.Method.Name);

            bool isExists = _cache.Contains(key);

            if (isExists)
            {
                invocation.ReturnValue = _cache.Get<object>(key);
            }
            else
            {
                invocation.Proceed();
                var returnValue = invocation.ReturnValue;

                _cache.AddAsync(key, returnValue);
            }
        }
    }
}
