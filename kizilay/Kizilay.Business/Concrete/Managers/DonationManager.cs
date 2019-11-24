using Kizilay.Business.Abstract;
using Kizilay.Business.Concrete.Result;
using Kizilay.Core.Aspects.Ninject.Cache;
using Kizilay.DataAccess.Abstract;
using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Business.Concrete.Managers
{
    public class DonationManager : IDonationManager
    {
        private IDonationDal _donationDal { get; set; }

        public DonationManager(IDonationDal donationDal)
        {
            _donationDal = donationDal;
        }

        [RemoveCacheAspect]
        public LayerResult<Donation> Add(Donation donation)
        {
            var result = new LayerResult<Donation>()
            {
                Result = donation
            };

            if (string.IsNullOrEmpty(donation.Name))
            {
                result.AddError("Lütfen geçerli değerler giriniz!");
                return result;
            }

            int count = _donationDal.Add(donation);

            if (count == 0)
                result.AddError("Ekleme işlemi başarısız!");

            return result;
        }

        [CacheAspect(cacheDuration: 30)]
        public List<Donation> GetAll()
        {
            return _donationDal.GetAll();
        }

        public bool IsExistsById(int Id)
        {
            return _donationDal.GetById(Id) != null;
        }

        [RemoveCacheAspect]
        public LayerResult<Donation> RemoveById(int Id)
        {
            var result = new LayerResult<Donation>();

            if (!IsExistsById(Id))
            {
                result.AddError("Böyle bir güvenlik kaydı bulunmamaktadır!");
                return result;
            }

            var count = _donationDal.RemoveById(Id);

            if (count == 0)
                result.AddError("Kayıt silinemedi!");

            return result;
        }

        [RemoveCacheAspect]
        public LayerResult<Donation> Update(Donation donation)
        {
            var result = new LayerResult<Donation>
            {
                Result = donation
            };

            if (donation.Id <= 0 || donation.DonationID < 0 || string.IsNullOrEmpty(donation.Name))
            {
                result.AddError("Lütfen geçerli değerler giriniz!");
                return result;
            }

            int count = _donationDal.Update(donation);

            if (count == 0)
                result.AddError("Güncelleme işlemi başarısız!");

            return result;
        }

        public Donation GetById(int Id)
        {
            return _donationDal.GetById(Id);
        }
    }
}
