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
    public class EducationalStatusManager : IEducationalStatusManager
    {
        private IEducationalStatusDal _educationalStatusDal { get; set; }

        public EducationalStatusManager(IEducationalStatusDal educationalStatusDal)
        {
            _educationalStatusDal = educationalStatusDal;
        }

        [RemoveCacheAspect]
        public LayerResult<EducationalStatus> Add(EducationalStatus educationalStatus)
        {
            var result = new LayerResult<EducationalStatus>()
            {
                Result = educationalStatus
            };

            if (string.IsNullOrEmpty(educationalStatus.Name))
            {
                result.AddError("Lütfen geçerli değerler giriniz!");
                return result;
            }

            int count = _educationalStatusDal.Add(educationalStatus);

            if (count == 0)
                result.AddError("Ekleme işlemi başarısız!");

            return result;
        }

        [CacheAspect(cacheDuration: 30)]
        public List<EducationalStatus> GetAll()
        {
            return _educationalStatusDal.GetAll();
        }

        public bool IsExistsById(int Id)
        {
            return _educationalStatusDal.GetById(Id) != null;
        }

        [RemoveCacheAspect]
        public LayerResult<EducationalStatus> RemoveById(int Id)
        {
            var result = new LayerResult<EducationalStatus>();

            if (!IsExistsById(Id))
            {
                result.AddError("Böyle bir güvenlik kaydı bulunmamaktadır!");
                return result;
            }

            var count = _educationalStatusDal.RemoveById(Id);

            if (count == 0)
                result.AddError("Kayıt silinemedi!");

            return result;
        }

        [RemoveCacheAspect]
        public LayerResult<EducationalStatus> Update(EducationalStatus educationalStatus)
        {
            var result = new LayerResult<EducationalStatus>
            {
                Result = educationalStatus
            };

            if (educationalStatus.Id <= 0 || string.IsNullOrEmpty(educationalStatus.Name))
            {
                result.AddError("Lütfen geçerli değerler giriniz!");
                return result;
            }

            int count = _educationalStatusDal.Update(educationalStatus);

            if (count == 0)
                result.AddError("Güncelleme işlemi başarısız!");

            return result;
        }

        public int GetEducationalStatusIdByName(string educationalStatusName)
        {
            return _educationalStatusDal.GetEducationalStatusIdByName(educationalStatusName);
        }
    }
}
