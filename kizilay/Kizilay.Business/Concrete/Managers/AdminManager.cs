using Kizilay.Business.Abstract;
using Kizilay.Business.Concrete.Result;
using Kizilay.Core.HashService;
using Kizilay.Core.ViewModels.Password;
using Kizilay.DataAccess.Abstract;
using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Business.Concrete.Managers
{
    public class AdminManager : IAdminManager
    {
        private IAdminDal _adminDal { get; set; }
        private IHashGeneratorService _hashGeneratorService { get; set; }

        public AdminManager(IAdminDal adminDal, IHashGeneratorService hashGeneratorService)
        {
            _hashGeneratorService = hashGeneratorService;
            _adminDal = adminDal;
        }

        public bool AdminIsExistsByUsernameAndPassword(string username, string password)
        {
            string hashedPassword = _hashGeneratorService.GenerateHash(password);

            return _adminDal.AdminIsExistsByUsernameAndPassword(username, hashedPassword);
        }

        public LayerResult<ChangePasswordViewModel> ChangePasswordByViewModel(ChangePasswordViewModel model)
        {
            LayerResult<ChangePasswordViewModel> layerResult = new LayerResult<ChangePasswordViewModel>();
            layerResult.Result = model;

            if (model.NewPassword != model.NewRePassword)
            {
                layerResult.AddError("Girilen parolalar eşleşmemektedir!");
                return layerResult;
            }

            var isExists = AdminIsExistsByUsernameAndPassword(model.Username, model.LastPassword);

            if (!isExists)
            {
                layerResult.AddError("Geçersiz kullanıcı adı veya parola!");
                return layerResult;
            }

            model.LastPassword = _hashGeneratorService.GenerateHash(model.LastPassword);
            model.NewPassword = _hashGeneratorService.GenerateHash(model.NewPassword);

            var count = _adminDal.ChangePasswordByViewModel(model);

            if (count == 0)
                layerResult.AddError("Güncelleme işlemi başarısız!");

            return layerResult;
        }
    }
}
