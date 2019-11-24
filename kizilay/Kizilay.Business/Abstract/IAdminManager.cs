using Kizilay.Business.Concrete.Result;
using Kizilay.Core.ViewModels.Password;
using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Business.Abstract
{
    public interface IAdminManager
    {
        bool AdminIsExistsByUsernameAndPassword(string username, string password);
        LayerResult<ChangePasswordViewModel> ChangePasswordByViewModel(ChangePasswordViewModel model);
    }
}
