using Kizilay.Business.Concrete.Result;
using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Business.Abstract
{
    public interface IDonationManager
    {
        bool IsExistsById(int Id);
        Donation GetById(int Id);
        List<Donation> GetAll();
        LayerResult<Donation> Add(Donation donation);
        LayerResult<Donation> Update(Donation donation);
        LayerResult<Donation> RemoveById(int Id);
    }
}
