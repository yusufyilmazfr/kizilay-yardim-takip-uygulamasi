using Kizilay.Core.Extensions.DataRowExtensions;
using Kizilay.DataAccess.Abstract;
using Kizilay.DataAccess.Concrete.AdoNet.Repository;
using Kizilay.DataAccess.Concrete.AdoNet.SqlHelper;
using Kizilay.Entities.Concrete;
using Kizilay.Entities.Concrete.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.DataAccess.Concrete.AdoNet
{
    public class AdoNetPersonDonationDal : AdoNetRepository<Person_Donation>, IPersonDonationDal
    {
        public AdoNetPersonDonationDal(AdoNetDbHelper adoNetDbHelper) : base(adoNetDbHelper) { }

        public override string TableName { get { return "Person_Donation"; } set { } }

        public int Add(Person_Donation personDonation)
        {
            var command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            command.CommandText =
                $"INSERT INTO {TableName} (PersonId,DonationId,AddedDate,Description) values (@personId,@donationId,@addedDate,@description)";

            command.Parameters.AddWithValue("@personId", personDonation.PersonId);
            command.Parameters.AddWithValue("@donationId", personDonation.DonationId);
            command.Parameters.AddWithValue("@addedDate", personDonation.AddedDate.Date);
            command.Parameters.AddWithValue("@description", personDonation.Description);

            _adoNetDbHelper.OpenSafeConnection();

            int count = command.ExecuteNonQuery();

            _adoNetDbHelper.CloseSafeConnection();

            command.Dispose();

            return count;
        }

        public List<PersonDonationHistory> GetFamilyDonationHistoriesByFamilyId(int familyId)
        {
            _adoNetDbHelper.command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            _adoNetDbHelper.command.CommandText = "SELECT " +
                "P.Name as PersonName, " +
                "P.Surname as PersonLastName, " +
                "PD.Id as DonationId, " +
                "PD.AddedDate as DonationDate, " +
                "D.Name as DonationName , " +
                "PD.Description as Description " +
                "FROM ((Person as P " +
                "INNER JOIN Family as F ON P.FamilyId = F.Id) " +
                "INNER JOIN Person_Donation as PD ON P.Id = PD.personId) " +
                "INNER JOIN Donation as D ON D.Id = PD.DonationId WHERE F.Id=@familyId";

            _adoNetDbHelper.command.Parameters.AddWithValue("@familyId", familyId);

            var dataTable = _adoNetDbHelper.GetDataTable();

            List<PersonDonationHistory> personDonationHistories = new List<PersonDonationHistory>();

            foreach (DataRow row in dataTable.Rows)
            {
                var donation = row.Map<PersonDonationHistory>();
                personDonationHistories.Add(donation);
            }

            dataTable.Dispose();

            return personDonationHistories;
        }

        public int Update(Person_Donation personDonation)
        {
            var command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            command.CommandText = $"UPDATE {TableName} SET AddedDate=@addedDate, Description=@description WHERE Id=@id";

            command.Parameters.AddWithValue("@addedDate", personDonation.AddedDate);
            command.Parameters.AddWithValue("@description", personDonation.Description);
            command.Parameters.AddWithValue("@id", personDonation.Id);

            _adoNetDbHelper.OpenSafeConnection();
            
            int count = command.ExecuteNonQuery();

            _adoNetDbHelper.CloseSafeConnection();
            
            command.Dispose();

            return count;
        }
    }
}
