using Kizilay.Core.CrossCuttingConcerns.Cache;
using Ninject.Extensions.Interception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Core.Aspects.Ninject.Cache
{
    public class RemoveCacheInterceptor : IInterceptor
    {
        public string Pattern { get; set; }

        private ICache _cache { get; set; }

        public RemoveCacheInterceptor(ICache cache)
        {
            _cache = cache;
        }

        public void Intercept(IInvocation invocation)
        {
            string nameSpaceWithClassName = String.Format("{0}.{1}*",
                    invocation.Request.Method.ReflectedType.Namespace,
                    invocation.Request.Method.ReflectedType.Name);

            _cache.RemoveByPattern(String.IsNullOrEmpty(Pattern) ? nameSpaceWithClassName : Pattern);

            invocation.Proceed();
        }
    }
}
