using Kizilay.DataAccess.Abstract;
using Kizilay.DataAccess.Concrete.AdoNet.Repository;
using Kizilay.DataAccess.Concrete.AdoNet.SqlHelper;
using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.DataAccess.Concrete.AdoNet
{
    public class AdoNetCityDal : AdoNetRepository<City>, ICityDal
    {
        public AdoNetCityDal(AdoNetDbHelper adoNetDbHelper) : base(adoNetDbHelper) { }

        public override string TableName { get { return "Cities"; } set { } }
    }
}
