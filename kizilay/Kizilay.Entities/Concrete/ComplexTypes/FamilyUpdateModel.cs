using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Entities.Concrete.ComplexTypes
{
    public class FamilyUpdateModel
    {
        public string LastTCNo { get; set; }
        public string FatherTCNo { get; set; }
        public Person Person { get; set; }
    }
}
