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
    public class AdoNetNeighborhoodDal : AdoNetRepository<Neighborhood>, INeighborhoodDal
    {
        public AdoNetNeighborhoodDal(AdoNetDbHelper adoNetDbHelper) : base(adoNetDbHelper) { }

        public override string TableName { get { return "Neighborhoods"; } set { } }

        public List<Neighborhood> GetAllASCByTownId(int townId)
        {
            _adoNetDbHelper.command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            _adoNetDbHelper.command.CommandText = $"SELECT * FROM {TableName}  WHERE TownId = @townId ORDER BY Name";
            _adoNetDbHelper.command.Parameters.AddWithValue("@townId", townId);

            var dataTable = _adoNetDbHelper.GetDataTable();

            var entities = new List<Neighborhood>();

            foreach (DataRow row in dataTable.Rows)
            {
                var entity = row.Map<Neighborhood>();

                entities.Add(entity);
            }

            dataTable.Dispose();

            return entities;
        }

        public int GetNeighborhoodIdByName(string neighborhoodName)
        {
            _adoNetDbHelper.command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            _adoNetDbHelper.command.CommandText = $"SELECT Id as xx FROM {TableName} WHERE Name = @name";
            _adoNetDbHelper.command.Parameters.AddWithValue("@name", neighborhoodName);

            _adoNetDbHelper.OpenSafeConnection();
            
            var reader = _adoNetDbHelper.command.ExecuteReader();

            int id = 0;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    id = (int)reader["xx"];
                }
            }

            reader.Close();

            _adoNetDbHelper.CloseSafeConnection();

            return id;
        }
    }
}
