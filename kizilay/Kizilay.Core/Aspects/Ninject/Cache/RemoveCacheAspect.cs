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
    public class RemoveCacheAspect : InterceptAttribute
    {
        private string _pattern;

        public RemoveCacheAspect(string pattern = "")
        {
            _pattern = pattern;
        }

        public override IInterceptor CreateInterceptor(IProxyRequest request)
        {
            var interceptor = request.Context.Kernel.Get<RemoveCacheInterceptor>();

            interceptor.Pattern = _pattern;

            return interceptor;
        }
    }
}
