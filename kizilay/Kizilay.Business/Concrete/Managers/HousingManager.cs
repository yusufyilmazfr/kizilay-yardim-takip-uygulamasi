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
    public class HousingManager : IHousingManager
    {
        private IHousingDal _housingDal { get; set; }

        public HousingManager(IHousingDal housingDal)
        {
            _housingDal = housingDal;
        }

        public bool IsExistsById(int Id)
        {
            return _housingDal.GetById(Id) != null;
        }

        [CacheAspect(cacheDuration: 30)]
        public List<Housing> GetAll()
        {
            return _housingDal.GetAll();
        }

        [RemoveCacheAspect]
        public LayerResult<Housing> Add(Housing housing)
        {
            var result = new LayerResult<Housing>()
            {
                Result = housing
            };

            if (string.IsNullOrEmpty(housing.Name))
            {
                result.AddError("Lütfen geçerli değerler giriniz!");
                return result;
            }

            int count = _housingDal.Add(housing);

            if (count == 0)
                result.AddError("Ekleme işlemi başarısız!");

            return result;
        }

        [RemoveCacheAspect]
        public LayerResult<Housing> Update(Housing housing)
        {
            var result = new LayerResult<Housing>
            {
                Result = housing
            };

            if (housing.Id <= 0 || string.IsNullOrEmpty(housing.Name))
            {
                result.AddError("Lütfen geçerli değerler giriniz!");
                return result;
            }

            int count = _housingDal.Update(housing);

            if (count == 0)
                result.AddError("Güncelleme işlemi başarısız!");

            return result;
        }

        [RemoveCacheAspect]
        public LayerResult<Housing> RemoveById(int Id)
        {
            var result = new LayerResult<Housing>();

            if (!IsExistsById(Id))
            {
                result.AddError("Böyle bir güvenlik kaydı bulunmamaktadır!");
                return result;
            }

            var count = _housingDal.RemoveById(Id);

            if (count == 0)
                result.AddError("Kayıt silinemedi!");

            return result;
        }
    }
}
