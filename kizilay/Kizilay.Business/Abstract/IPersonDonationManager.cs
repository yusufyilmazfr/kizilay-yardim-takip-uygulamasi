using Kizilay.Business.Concrete.Result;
using Kizilay.Entities.Concrete;
using Kizilay.Entities.Concrete.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Business.Abstract
{
    public interface IPersonDonationManager
    {
        LayerResult<Person_Donation> Add(Person_Donation personDonation);
        Person_Donation GetById(int Id);
        LayerResult<Person_Donation> Update(Person_Donation personDonation);
        List<PersonDonationHistory> GetFamilyDonationHistoriesByFamilyId(int familyId);
        int RemoveById(int donationId);
    }
}
