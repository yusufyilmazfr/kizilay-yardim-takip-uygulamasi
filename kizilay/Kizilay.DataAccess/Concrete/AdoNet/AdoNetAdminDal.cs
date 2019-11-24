using Kizilay.Core.ViewModels.Password;
using Kizilay.DataAccess.Abstract;
using Kizilay.DataAccess.Concrete.AdoNet.Repository;
using Kizilay.DataAccess.Concrete.AdoNet.SqlHelper;
using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.DataAccess.Concrete.AdoNet
{
    public class AdoNetAdminDal : AdoNetRepository<Admin>, IAdminDal
    {
        public AdoNetAdminDal(AdoNetDbHelper adoNetDbHelper) : base(adoNetDbHelper) { }

        public override string TableName { get { return "Admin"; } set { } }


        public bool AdminIsExistsByUsernameAndPassword(string username, string password)
        {
            var command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            command.CommandText = "SELECT * FROM Admin WHERE Username=@username AND Passwd=@password";

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            bool isExists = _adoNetDbHelper.Any(command);

            return isExists;
        }

        public int ChangePasswordByViewModel(ChangePasswordViewModel model)
        {
            var command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            command.CommandText = $"UPDATE Admin SET Passwd='{model.NewPassword}' WHERE Username=@uname AND Passwd=@lastPasswd";

            command.Parameters.AddWithValue("@uname", model.Username);
            command.Parameters.AddWithValue("@lastPasswd", model.LastPassword);
            command.Parameters.AddWithValue("@newPasswd", model.NewPassword);

            _adoNetDbHelper.OpenSafeConnection();
            
            int count = command.ExecuteNonQuery();

            _adoNetDbHelper.CloseSafeConnection();
            
            return count;
        }
    }
}
