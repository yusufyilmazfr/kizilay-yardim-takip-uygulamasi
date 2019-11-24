using Kizilay.Business.Concrete.BusinessModule.Ninject;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kizilay.DependencyResolver.Ninject
{
    public static class FormDependencyResolver
    {
        private static IKernel _kernel { get; set; }

        public static Form Resolve<T>()
        {
            if (_kernel == null)
            {
                _kernel = new StandardKernel(new NinjectSettings { LoadExtensions = true }, new InstanceModule());

            }

            var form = _kernel.Get<T>();

            return form as Form;
        }
    }
}
