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
    public interface IFamilyDal : IRepository<Family>
    {
        int Add(Family family);
        int Update(Family family);
        int UpdateByFatherTCNo(Family family);
        bool FamilyExistsByFatherTCNo(string fatherTCNo);
        bool FamilyHasDonationInRangeDayByFatherTCNo(int familyId, int rangeOfDay);
        Family GetFamilyByFatherTCNo(string fatherTCNo);
        FamilyWithAddress GetFamilyWithAddressByFatherTCNo(string fatherTCNo);
    }
}
