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
    public class SocialSecurityManager : ISocialSecurityManager
    {
        private ISocialSecurityDal _socialSecurityDal { get; set; }

        public SocialSecurityManager(ISocialSecurityDal socialSecurityDal)
        {
            _socialSecurityDal = socialSecurityDal;
        }

        [CacheAspect(cacheDuration: 30)]
        public List<SocialSecurity> GetAll()
        {
            return _socialSecurityDal.GetAll();
        }

        [RemoveCacheAspect]
        public LayerResult<SocialSecurity> Update(SocialSecurity socialSecurity)
        {
            var result = new LayerResult<SocialSecurity>
            {
                Result = socialSecurity
            };

            if (socialSecurity.Id <= 0 || string.IsNullOrEmpty(socialSecurity.Name))
            {
                result.AddError("Lütfen geçerli değerler giriniz!");
                return result;
            }

            int count = _socialSecurityDal.Update(socialSecurity);

            if (count == 0)
                result.AddError("Güncelleme işlemi başarısız!");

            return result;
        }

        [RemoveCacheAspect]
        public LayerResult<SocialSecurity> Add(SocialSecurity socialSecurity)
        {
            var result = new LayerResult<SocialSecurity>()
            {
                Result = socialSecurity
            };

            if (string.IsNullOrEmpty(socialSecurity.Name))
            {
                result.AddError("Lütfen geçerli değerler giriniz!");
                return result;
            }

            int count = _socialSecurityDal.Add(socialSecurity);

            if (count == 0)
                result.AddError("Ekleme işlemi başarısız!");

            return result;
        }

        public bool IsExistsById(int Id)
        {
            return _socialSecurityDal.GetById(Id) != null;
        }

        [RemoveCacheAspect]
        public LayerResult<SocialSecurity> RemoveById(int Id)
        {
            var result = new LayerResult<SocialSecurity>();

            if (!IsExistsById(Id))
            {
                result.AddError("Böyle bir güvenlik kaydı bulunmamaktadır!");
                return result;
            }

            var count = _socialSecurityDal.RemoveById(Id);

            if (count == 0)
                result.AddError("Kayıt silinemedi!");

            return result;
        }

        public int GetSocialSecurityIdByName(string socialSecurityName)
        {
            return _socialSecurityDal.GetSocialSecurityIdByName(socialSecurityName);
        }
    }
}
