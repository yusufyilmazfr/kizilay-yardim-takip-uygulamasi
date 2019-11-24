using Kizilay.DataAccess.Abstract.Repository;
using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.DataAccess.Abstract
{
    public interface INeighborhoodDal : IRepository<Neighborhood>
    {
        List<Neighborhood> GetAllASCByTownId(int townId);
        int GetNeighborhoodIdByName(string neighborhoodName);
    }
}
