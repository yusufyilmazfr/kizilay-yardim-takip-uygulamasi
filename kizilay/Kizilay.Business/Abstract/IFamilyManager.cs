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
    public interface IFamilyManager
    {
        int Add(Family family);
        int Update(Family family);
        LayerResult<Family> UpdateByFatherTCNo(Family family);
        Family GetFamilyByFatherTCNo(string fatherTCNo);
        bool FamilyExistsByFatherTCNo(string fatherTCNo);
        bool FamilyHasDonationInRangeDayByFamilyId(int familyId, int rangeOfDay);
        FamilyWithAddress GetFamilyWithAddressByFatherTCNo(string fatherTCNo);
    }
}
