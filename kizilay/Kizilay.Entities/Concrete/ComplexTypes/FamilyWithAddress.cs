using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Entities.Concrete.ComplexTypes
{
    public class FamilyWithAddress
    {
        public string FatherNo { get; set; }
        public int PersonCount { get; set; }
        public int Priority { get; set; }
        public string Address { get; set; }


        public int HousingId { get; set; }
        //public string HousingName { get; set; }

        public int NeighborhoodsId { get; set; }
        public string NeighborhoodsName { get; set; }

        public int CityId { get; set; }
        public string CityName { get; set; }

        public int TownId { get; set; }
        public string TownName { get; set; }
    }
}
