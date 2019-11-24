using Kizilay.Core.Extensions.DataRowExtensions;
using Kizilay.DataAccess.Abstract;
using Kizilay.DataAccess.Concrete.AdoNet.Repository;
using Kizilay.DataAccess.Concrete.AdoNet.SqlHelper;
using Kizilay.Entities.Concrete;
using Kizilay.Entities.Concrete.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.DataAccess.Concrete.AdoNet
{
    public class AdoNetFamilyDal : AdoNetRepository<Family>, IFamilyDal
    {
        public AdoNetFamilyDal(AdoNetDbHelper adoNetDbHelper) : base(adoNetDbHelper) { }

        public override string TableName { get { return "Family"; } set { } }

        public int Add(Family family)
        {
            var command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            command.CommandText = $"INSERT INTO {TableName} (FatherNo,HousingId,Priority,PersonCount,Address,NeighborhoodsId) values (@fatherNo,@housingId,@priority,@personCount,@address,@neighborhoodsId)";


            command.Parameters.AddWithValue("@fatherNo", family.FatherNo);
            command.Parameters.AddWithValue("@housingId", family.HousingId);
            command.Parameters.AddWithValue("@priority", family.Priority);
            command.Parameters.AddWithValue("@personCount", family.PersonCount);
            command.Parameters.AddWithValue("@address", family.Address);
            command.Parameters.AddWithValue("@neighborhoodsId", family.NeighborhoodsId);

            _adoNetDbHelper.OpenSafeConnection();

            int count = command.ExecuteNonQuery();

            _adoNetDbHelper.CloseSafeConnection();

            command.Dispose();
            return count;
        }

        public int Update(Family family)
        {
            var command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            command.CommandText = $"UPDATE {TableName} SET Priority=@priority, HousingId=@housingId,PersonCount=@personCount,Address=@address,NeighborhoodsId=@neighborhoodsId,FatherNo=@fatherNo WHERE Id=@Id";

            command.Parameters.AddWithValue("@priority", family.Priority);
            command.Parameters.AddWithValue("@housingId", family.HousingId);
            command.Parameters.AddWithValue("@personCount", family.PersonCount);
            command.Parameters.AddWithValue("@address", family.Address);
            command.Parameters.AddWithValue("@neighborhoodsId", family.NeighborhoodsId);
            command.Parameters.AddWithValue("@fatherNo", family.FatherNo);
            command.Parameters.AddWithValue("@Id", family.Id);

            _adoNetDbHelper.OpenSafeConnection();

            int count = command.ExecuteNonQuery();

            _adoNetDbHelper.CloseSafeConnection();

            command.Dispose();

            return count;
        }

        public int UpdateByFatherTCNo(Family family)
        {
            var command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            command.CommandText = $"UPDATE {TableName} SET Priority=@priority, HousingId=@housingId,PersonCount=@personCount,Address=@address,NeighborhoodsId=@neighborhoodsId WHERE FatherNo=@fatherNo";

            command.Parameters.AddWithValue("@priority", family.Priority);
            command.Parameters.AddWithValue("@housingId", family.HousingId);
            command.Parameters.AddWithValue("@personCount", family.PersonCount);
            command.Parameters.AddWithValue("@address", family.Address);
            command.Parameters.AddWithValue("@neighborhoodsId", family.NeighborhoodsId);
            command.Parameters.AddWithValue("@fatherNo", family.FatherNo);

            _adoNetDbHelper.OpenSafeConnection();

            int count = command.ExecuteNonQuery();

            _adoNetDbHelper.CloseSafeConnection();

            command.Dispose();

            return count;
        }

        public bool FamilyExistsByFatherTCNo(string fatherTCNo)
        {
            _adoNetDbHelper.command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            _adoNetDbHelper.command.CommandText = $"SELECT * FROM {TableName} WHERE FatherNo = @fatherNo";
            _adoNetDbHelper.command.Parameters.AddWithValue("@fatherNo", fatherTCNo);

            var dataTable = _adoNetDbHelper.GetDataTable();

            return dataTable.Rows.Count > 0;
        }

        public Family GetFamilyByFatherTCNo(string fatherTCNo)
        {
            _adoNetDbHelper.command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            _adoNetDbHelper.command.CommandText = $"SELECT * FROM {TableName} WHERE FatherNo = @fatherNo";
            _adoNetDbHelper.command.Parameters.AddWithValue("@fatherNo", fatherTCNo);

            _adoNetDbHelper.OpenSafeConnection();

            var dataTable = _adoNetDbHelper.GetDataTable();

            if (dataTable.Rows == null || dataTable.Rows.Count == 0)
                return null;

            var family = dataTable.Rows[0].Map<Family>();


            _adoNetDbHelper.CloseSafeConnection();

            return family;
        }

        public FamilyWithAddress GetFamilyWithAddressByFatherTCNo(string fatherTCNo)
        {
            _adoNetDbHelper.command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            _adoNetDbHelper.command.CommandText = "SELECT TOP 1 " +
                        "F.PersonCount as PersonCount," +
                    "F.Priority as Priority," +
                    "F.Address as Address," +
                    "F.HousingId as HousingId," +
                    "F.FatherNo as FatherNo," +
                    "F.NeighborhoodsId as NeighborhoodsId," +
                    "N.Name as NeighborhoodsName," +
                    "C.Id as CityId," +
                    "C.Name as CityName," +
                    "T.Id as TownId," +
                    "T.Name as TownName " +
                        "FROM (((Family as F INNER JOIN Neighborhoods as N ON F.NeighborhoodsId = N.Id)" +
                        "INNER JOIN Towns as T ON N.TownId = T.Id)" +
                        "INNER JOIN Cities AS C ON C.Id = T.CityId) WHERE FatherNo=@fatherNo";

            _adoNetDbHelper.command.Parameters.AddWithValue("@fatherNo", fatherTCNo);

            var dataTable = _adoNetDbHelper.GetDataTable();

            var familyWithAddress = dataTable.Rows[0].Map<FamilyWithAddress>();

            dataTable.Dispose();

            return familyWithAddress;
        }

        public bool FamilyHasDonationInRangeDayByFatherTCNo(int familyId, int rangeOfDay)
        {
            _adoNetDbHelper.command = _adoNetDbHelper.connection.CreateCommand();

            _adoNetDbHelper.command.CommandText = $"SELECT * FROM ({TableName} INNER JOIN Person ON Family.Id = Person.FamilyId) INNER JOIN(Donation INNER JOIN Person_Donation ON Donation.Id = Person_Donation.DonationId) ON Person.Id = Person_Donation.PersonId WHERE Family.Id = {familyId} AND DateDiff('d', Person_Donation.AddedDate, Now) < {rangeOfDay};";


            var dataTable = _adoNetDbHelper.GetDataTable();

            bool hasRow = dataTable.Rows.Count > 0;

            dataTable.Dispose();

            return hasRow;
        }
    }
}
