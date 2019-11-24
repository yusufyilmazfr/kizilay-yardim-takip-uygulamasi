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
    public class AdoNetDonationDal : AdoNetRepository<Donation>, IDonationDal
    {
        public AdoNetDonationDal(AdoNetDbHelper adoNetDbHelper) : base(adoNetDbHelper) { }

        public override string TableName { get { return "Donation"; } set { } }

        public int Add(Donation donation)
        {
            var command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            command.CommandText = $"INSERT INTO {TableName} (Name,DonationID) values(@donationName,@donationParentId)";

            command.Parameters.AddWithValue("@donationName", donation.Name);
            command.Parameters.AddWithValue("@donationParentId", donation.DonationID);

            _adoNetDbHelper.OpenSafeConnection();
            
            int count = command.ExecuteNonQuery();

            _adoNetDbHelper.CloseSafeConnection();
            
            command.Dispose();

            return count;
        }

        public int Update(Donation donation)
        {
            var command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            command.CommandText =
                $"UPDATE {TableName} SET Name=@donationName,DonationID=@donationParentID WHERE Id=@donationId";

            command.Parameters.AddWithValue("@donationName", donation.Name);
            command.Parameters.AddWithValue("@donationParentID", donation.DonationID);
            command.Parameters.AddWithValue("@donationId", donation.Id);

            _adoNetDbHelper.OpenSafeConnection();
            
            int count = command.ExecuteNonQuery();

            _adoNetDbHelper.CloseSafeConnection();
            
            command.Dispose();

            return count;
        }
    }
}
