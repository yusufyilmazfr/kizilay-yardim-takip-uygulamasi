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
    public class CityManager : ICityManager
    {
        private ICityDal _cityDal { get; set; }

        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }

        public List<City> GetAll()
        {
            return _cityDal.GetAll();
        }
    }
}
