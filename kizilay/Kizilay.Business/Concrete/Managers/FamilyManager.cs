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
    public class FamilyManager : IFamilyManager
    {
        private IFamilyDal _familyDal { get; set; }

        public FamilyManager(IFamilyDal familyDal)
        {
            _familyDal = familyDal;
        }

        public bool FamilyExistsByFatherTCNo(string fatherTCNo)
        {
            return _familyDal.FamilyExistsByFatherTCNo(fatherTCNo);
        }

        public int Add(Family family)
        {
            if (String.IsNullOrEmpty(family.FatherNo) || family.FatherNo.Length != 11 || String.IsNullOrEmpty(family.Address))
                return 0;

            return _familyDal.Add(family);
        }

        public Family GetFamilyByFatherTCNo(string fatherTCNo)
        {
            return _familyDal.GetFamilyByFatherTCNo(fatherTCNo);
        }

        public FamilyWithAddress GetFamilyWithAddressByFatherTCNo(string fatherTCNo)
        {
            return _familyDal.GetFamilyWithAddressByFatherTCNo(fatherTCNo);
        }

        public LayerResult<Family> UpdateByFatherTCNo(Family family)
        {
            var layerResult = new LayerResult<Family>() { Result = family };

            if (family.FatherNo.Length != 11)
            {
                layerResult.AddError("T.C numarası 11 karakter olmalıdır!");
                return layerResult;
            }
            if (String.IsNullOrEmpty(family.Address))
            {
                layerResult.AddError("Adres boş olmamalıdır!!");
                return layerResult;
            }

            int count = _familyDal.UpdateByFatherTCNo(family);

            if (count == 0)
                layerResult.AddError("Güncelleme işlemi gerçekleştirilemedi, girdiğiniz değerleri kontrol ediniz!");

            return layerResult;
        }

        public bool FamilyHasDonationInRangeDayByFamilyId(int familyId, int rangeOfDay)
        {
            return _familyDal.FamilyHasDonationInRangeDayByFatherTCNo(familyId, rangeOfDay);
        }

        public int Update(Family family)
        {
            return _familyDal.Update(family);
        }
    }
}
