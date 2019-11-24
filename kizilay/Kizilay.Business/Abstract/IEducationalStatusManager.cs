using Kizilay.Business.Concrete.Result;
using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Business.Abstract
{
    public interface IEducationalStatusManager
    {
        bool IsExistsById(int Id);
        List<EducationalStatus> GetAll();
        int GetEducationalStatusIdByName(string educationalStatusName);
        LayerResult<EducationalStatus> Add(EducationalStatus educationalStatus);
        LayerResult<EducationalStatus> Update(EducationalStatus educationalStatus);
        LayerResult<EducationalStatus> RemoveById(int Id);
    }
}
