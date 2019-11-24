using Kizilay.DataAccess.Abstract.Repository;
using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.DataAccess.Abstract
{
    public interface IDonationDal : IRepository<Donation>
    {
        int Add(Donation donation);
        int Update(Donation donation);
    }
}
