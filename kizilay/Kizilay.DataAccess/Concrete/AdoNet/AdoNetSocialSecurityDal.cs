using Kizilay.DataAccess.Abstract;
using Kizilay.DataAccess.Concrete.AdoNet.Repository;
using Kizilay.DataAccess.Concrete.AdoNet.SqlHelper;
using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.DataAccess.Concrete.AdoNet
{
    public class AdoNetSocialSecurityDal : AdoNetRepository<SocialSecurity>, ISocialSecurityDal
    {
        public AdoNetSocialSecurityDal(AdoNetDbHelper adoNetDbHelper) : base(adoNetDbHelper) { }

        public override string TableName { get; set; } = "SocialSecurity";

        public int Add(SocialSecurity socialSecurity)
        {
            var command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            command.CommandText = $"INSERT INTO {TableName} (Name) values(@socialSecurityName)";

            command.Parameters.AddWithValue("@socialSecurityName", socialSecurity.Name);

            _adoNetDbHelper.OpenSafeConnection();

            int count = command.ExecuteNonQuery();

            _adoNetDbHelper.CloseSafeConnection();
            
            command.Dispose();
            return count;
        }

        public int GetSocialSecurityIdByName(string socialSecurityName)
        {
            _adoNetDbHelper.command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            _adoNetDbHelper.command.CommandText = $"SELECT Id FROM {TableName} WHERE Name = @name";
            _adoNetDbHelper.command.Parameters.AddWithValue("@name", socialSecurityName);

            var reader = _adoNetDbHelper.command.ExecuteReader();

            int id = 0;

            if (reader.HasRows)
            {
                id = reader.GetInt32(0);
            }

            return id;
        }

        public int Update(SocialSecurity socialSecurity)
        {
            var command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            command.CommandText = $"UPDATE {TableName} SET Name=@socialSecurityName WHERE Id=@socialSecurityId";

            command.Parameters.AddWithValue("@socialSecurityName", socialSecurity.Name);
            command.Parameters.AddWithValue("@socialSecurityId", socialSecurity.Id);

            _adoNetDbHelper.OpenSafeConnection();

            int count = command.ExecuteNonQuery();

            _adoNetDbHelper.CloseSafeConnection();
            
            command.Dispose();

            return count;
        }
    }
}
