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
    public class AdoNetHousingDal : AdoNetRepository<Housing>, IHousingDal
    {
        public AdoNetHousingDal(AdoNetDbHelper adoNetDbHelper) : base(adoNetDbHelper) { }

        public override string TableName { get { return "Housing"; } set { } }

        public int Add(Housing housing)
        {
            var command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            command.CommandText = $"INSERT INTO {TableName} (Name) values(@housingName)";

            command.Parameters.AddWithValue("@housingName", housing.Name);

            _adoNetDbHelper.OpenSafeConnection();
            
            int count = command.ExecuteNonQuery();

            _adoNetDbHelper.CloseSafeConnection();
            
            command.Dispose();

            return count;
        }

        public int Update(Housing housing)
        {
            var command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            command.CommandText = $"UPDATE {TableName} SET Name=@housingName WHERE Id=@housingId";

            command.Parameters.AddWithValue("@housingName", housing.Name);
            command.Parameters.AddWithValue("@housingId", housing.Id);

            _adoNetDbHelper.OpenSafeConnection();

            int count = command.ExecuteNonQuery();

            _adoNetDbHelper.CloseSafeConnection();
            
            command.Dispose();

            return count;
        }
    }
}
