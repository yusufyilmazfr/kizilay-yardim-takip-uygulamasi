using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Entities.Concrete
{
    public class Family : BaseEntity
    {
        [Required,
        DisplayName("Baba T.C"),
        StringLength(11,ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!")]
        public string FatherNo { get; set; }

        [Required]
        public int HousingId { get; set; }

        [Required]
        public int Priority { get; set; }

        [Required]
        public int PersonCount { get; set; }

        [Required,
        DisplayName("Adres"),
        MaxLength(255, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!")]
        public string Address { get; set; }

        [Required]
        public int NeighborhoodsId { get; set; }
    }
}
