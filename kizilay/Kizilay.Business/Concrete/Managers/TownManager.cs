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
    public class TownManager : ITownManager
    {
        private ITownDal _townDal { get; set; }

        public TownManager(ITownDal townDal)
        {
            _townDal = townDal;
        }

        public List<Town> GetAllTownsASCByCityId(int cityId)
        {
            return _townDal.GetAllTownsASCByCityId(cityId);
        }
    }
}
