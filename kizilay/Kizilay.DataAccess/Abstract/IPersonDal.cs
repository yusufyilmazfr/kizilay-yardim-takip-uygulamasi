using Kizilay.DataAccess.Abstract.Repository;
using Kizilay.Entities.Concrete;
using Kizilay.Entities.Concrete.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.DataAccess.Abstract
{
    public interface IPersonDal : IRepository<Person>
    {
        int Add(Person person);
        Person GetByTCNo(string personTCNo);
        DataTable GetAllPersonInformationForExport();
        bool PersonExistsByTCNo(string tcNo);
        int Update(FamilyUpdateModel model);
        int RemovePersonByTCNo(string tcNo);
    }
}
