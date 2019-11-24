using Kizilay.Core.ViewModels.Password;
using Kizilay.DataAccess.Abstract.Repository;
using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.DataAccess.Abstract
{
    public interface IAdminDal : IRepository<Admin>
    {
        bool AdminIsExistsByUsernameAndPassword(string username, string password);
        int ChangePasswordByViewModel(ChangePasswordViewModel model);
    }
}
