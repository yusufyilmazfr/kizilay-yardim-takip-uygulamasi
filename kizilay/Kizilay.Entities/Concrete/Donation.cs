using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Entities.Concrete
{
    public class Donation : BaseEntity
    {
        [Required,
        DisplayName("Bağış adı"),
        MaxLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!")]
        public string Name { get; set; }

        [Required]
        public int DonationID { get; set; }
    }
}
