using Kizilay.DataAccess.Abstract.Repository;
using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.DataAccess.Abstract
{
    public interface IHousingDal : IRepository<Housing>
    {
        int Add(Housing housing);
        int Update(Housing housing);
    }
}
