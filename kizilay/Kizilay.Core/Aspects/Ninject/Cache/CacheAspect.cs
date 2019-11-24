using Kizilay.Core.CrossCuttingConcerns.Cache;
using Ninject;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Attributes;
using Ninject.Extensions.Interception.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Core.Aspects.Ninject.Cache
{
    public class CacheAspect : InterceptAttribute
    {
        private int _cacheDuration;

        public CacheAspect(int cacheDuration = 60)
        {
            _cacheDuration = cacheDuration;
        }

        public override IInterceptor CreateInterceptor(IProxyRequest request)
        {
            var interceptor = request.Context.Kernel.Get<CacheInterceptor>();

            interceptor.CacheDuration = _cacheDuration;

            return interceptor;
        }
    }
}
