using Kizilay.Business.Abstract;
using Kizilay.Business.Concrete.Result;
using Kizilay.DataAccess.Abstract;
using Kizilay.Entities.Concrete;
using Kizilay.Entities.Concrete.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Business.Concrete.Managers
{
    public class PersonDonationManager : IPersonDonationManager
    {
        private IPersonDonationDal _personDonationDal { get; set; }

        public PersonDonationManager(IPersonDonationDal personDonationDal)
        {
            _personDonationDal = personDonationDal;
        }

        public LayerResult<Person_Donation> Update(Person_Donation personDonation)
        {
            LayerResult<Person_Donation> layerResult = new LayerResult<Person_Donation>();
            layerResult.Result = personDonation;

            if (String.IsNullOrEmpty(personDonation.Description))
            {
                layerResult.AddError("Bağış açıklaması boş geçilemez!");
                return layerResult;
            }

            int count = _personDonationDal.Update(personDonation);

            if (count == 0)
                layerResult.AddError("Güncelleme işlemi gerçekleştirilemedi!");

            return layerResult;
        }

        public Person_Donation GetById(int Id)
        {
            return _personDonationDal.GetById(Id);
        }

        public List<PersonDonationHistory> GetFamilyDonationHistoriesByFamilyId(int familyId)
        {
            return _personDonationDal.GetFamilyDonationHistoriesByFamilyId(familyId);
        }

        public LayerResult<Person_Donation> Add(Person_Donation personDonation)
        {
            LayerResult<Person_Donation> layerResult = new LayerResult<Person_Donation>();
            layerResult.Result = personDonation;

            if (String.IsNullOrEmpty(personDonation.Description))
                layerResult.AddError("Açıklama alanı boş bırakılamaz!");

            if (layerResult.HasError())
                return layerResult;

            int count = _personDonationDal.Add(personDonation);

            if (count == 0)
                layerResult.AddError("Ekleme işlemi başarısız!");

            return layerResult;
        }

        public int RemoveById(int donationId)
        {
            return _personDonationDal.RemoveById(donationId);
        }
    }
}
