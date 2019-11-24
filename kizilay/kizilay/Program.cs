using kizilay.DependencyResolver.Ninject;
using Kizilay.Business.Concrete.BusinessModule.Ninject;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kizilay
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var loginForm = FormDependencyResolver.Resolve<frmLogin>();

            Control.CheckForIllegalCrossThreadCalls = false;

            Application.EnableVisualStyles();

            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(loginForm);
        }
    }
}
