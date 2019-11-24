using Kizilay.DataAccess.Abstract.Repository;
using Kizilay.Entities.Concrete;
using Kizilay.Entities.Concrete.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.DataAccess.Abstract
{
    public interface IPersonDonationDal : IRepository<Person_Donation>
    {
        int Add(Person_Donation personDonation);
        int Update(Person_Donation personDonation);
        List<PersonDonationHistory> GetFamilyDonationHistoriesByFamilyId(int familyId);
    }
}
