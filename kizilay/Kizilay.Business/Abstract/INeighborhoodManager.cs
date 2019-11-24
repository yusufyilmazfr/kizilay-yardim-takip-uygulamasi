using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Business.Abstract
{
    public interface INeighborhoodManager
    {
        List<Neighborhood> GetAllASCByTownId(int townId);
        int GetNeighborhoodIdByName(string neighborhoodName);
    }
}
