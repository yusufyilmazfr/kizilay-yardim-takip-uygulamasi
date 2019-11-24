using Kizilay.Business.Concrete.Result;
using Kizilay.Entities.Concrete;
using Kizilay.Entities.Concrete.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Business.Abstract
{
    public interface IPersonManager
    {
        Person GetById(int Id);
        Person GetByTCNo(string personTCNo);
        bool PersonExistsByTCNo(string tcNo);
        DataTable GetAllPersonInformationForExport();
        LayerResult<Person> Add(Person person);
        LayerResult<Person> Update(FamilyUpdateModel model);
        LayerResult<Person> RemoveByTCNo(string tcNo);
    }
}
