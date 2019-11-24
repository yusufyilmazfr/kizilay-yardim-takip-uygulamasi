using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Core.ViewModels.Password
{
    public class ChangePasswordViewModel
    {
        public string Username { get; set; }
        public string LastPassword { get; set; }
        public string NewRePassword { get; set; }
        public string NewPassword { get; set; }
    }
}
