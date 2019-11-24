using Kizilay.DataAccess.Abstract.Repository;
using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.DataAccess.Abstract
{
    public interface IEducationalStatusDal : IRepository<EducationalStatus>
    {
        int Add(EducationalStatus educationalStatus);
        int Update(EducationalStatus educationalStatus);
        int GetEducationalStatusIdByName(string educationalStatusName);
    }
}
