using Kizilay.Core.Extensions.DataRowExtensions;
using Kizilay.DataAccess.Abstract;
using Kizilay.DataAccess.Concrete.AdoNet.Repository;
using Kizilay.DataAccess.Concrete.AdoNet.SqlHelper;
using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.DataAccess.Concrete.AdoNet
{
    public class AdoNetTownDal : AdoNetRepository<Town>, ITownDal
    {
        public AdoNetTownDal(AdoNetDbHelper adoNetDbHelper) : base(adoNetDbHelper) { }

        public override string TableName { get { return "Towns"; } set { } }

        public List<Town> GetAllTownsASCByCityId(int cityId)
        {
            _adoNetDbHelper.command = _adoNetDbHelper.CreateCommandInCurrentConnection();
            _adoNetDbHelper.command.CommandText = $"SELECT * FROM {TableName} WHERE CityId = @cityId ORDER BY Name";
            _adoNetDbHelper.command.Parameters.AddWithValue("@cityId", cityId);

            var dataTable = _adoNetDbHelper.GetDataTable();

            List<Town> entities = new List<Town>();

            foreach (DataRow row in dataTable.Rows)
            {
                var entity = row.Map<Town>();

                entities.Add(entity);
            }

            dataTable.Dispose();

            return entities;
        }
    }
}
