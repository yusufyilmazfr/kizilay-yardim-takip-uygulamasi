using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kizilay
{
    public class DonationProcessModel
    {
        public int donationId { get; set; }
        public string PersonName { get; set; }
        public string PersonLastName { get; set; }
        public string DonationName { get; set; }
        public DateTime DonationDate { get; set; }
        public string Description { get; set; }

    }
}
