using Kizilay.Business.Abstract;
using Kizilay.DataAccess.Abstract;
using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Business.Concrete.Managers
{
    public class NeighborhoodManager : INeighborhoodManager
    {
        private INeighborhoodDal _neighborhoodDal { get; set; }

        public NeighborhoodManager(INeighborhoodDal neighborhoodDal)
        {
            _neighborhoodDal = neighborhoodDal;
        }
        public List<Neighborhood> GetAllASCByTownId(int townId)
        {
            return _neighborhoodDal.GetAllASCByTownId(townId);
        }

        public int GetNeighborhoodIdByName(string neighborhoodName)
        {
            return _neighborhoodDal.GetNeighborhoodIdByName(neighborhoodName);
        }
    }
}
