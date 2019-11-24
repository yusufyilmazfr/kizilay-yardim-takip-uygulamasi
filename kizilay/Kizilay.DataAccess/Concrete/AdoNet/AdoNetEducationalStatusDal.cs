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
    public class AdoNetEducationalStatusDal : AdoNetRepository<EducationalStatus>, IEducationalStatusDal
    {
        public AdoNetEducationalStatusDal(AdoNetDbHelper adoNetDbHelper) : base(adoNetDbHelper) { }

        public override string TableName { get { return "EducationalStatus"; } set { } }

        public int Add(EducationalStatus educationalStatus)
        {
            var command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            command.CommandText = $"INSERT INTO {TableName} (Name) values(@educationalStatusName)";

            command.Parameters.AddWithValue("@educationalStatusName", educationalStatus.Name);

            _adoNetDbHelper.OpenSafeConnection();

            int count = command.ExecuteNonQuery();

            _adoNetDbHelper.CloseSafeConnection();

            command.Dispose();

            return count;
        }

        public int GetEducationalStatusIdByName(string educationalStatusName)
        {
            _adoNetDbHelper.command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            _adoNetDbHelper.command.CommandText = $"SELECT Id FROM {TableName} WHERE Name = @name";
            _adoNetDbHelper.command.Parameters.AddWithValue("@name", educationalStatusName);

            _adoNetDbHelper.OpenSafeConnection();

            var reader = _adoNetDbHelper.command.ExecuteReader();

            _adoNetDbHelper.CloseSafeConnection();

            int id = 0;

            if (reader.HasRows)
            {
                id = reader.GetInt32(0);
            }

            reader.Close();

            return id;
        }

        public int Update(EducationalStatus educationalStatus)
        {
            var command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            command.CommandText = $"UPDATE {TableName} SET Name=@educationalStatusName WHERE Id=@educationalStatusId";

            command.Parameters.AddWithValue("@educationalStatusName", educationalStatus.Name);
            command.Parameters.AddWithValue("@educationalStatusId", educationalStatus.Id);

            _adoNetDbHelper.OpenSafeConnection();

            int count = command.ExecuteNonQuery();

            _adoNetDbHelper.CloseSafeConnection();

            command.Dispose();

            return count;
        }
    }
}
