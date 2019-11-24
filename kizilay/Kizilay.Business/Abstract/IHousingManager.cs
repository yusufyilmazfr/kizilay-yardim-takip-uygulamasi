using Kizilay.Business.Concrete.Result;
using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Business.Abstract
{
    public interface IHousingManager
    {
        bool IsExistsById(int Id);
        List<Housing> GetAll();
        LayerResult<Housing> Add(Housing housing);
        LayerResult<Housing> Update(Housing housing);
        LayerResult<Housing> RemoveById(int Id);
    }
}
