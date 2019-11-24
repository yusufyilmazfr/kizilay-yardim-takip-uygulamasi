using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.DataAccess.Abstract.Repository
{
    public interface IRepository<T> where T : class, new()
    {
        T GetById(int Id);
        int RemoveById(int Id);
            
        List<T> GetAll();
    }
}