using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Entities.Concrete
{
    public class Neighborhood : BaseEntity
    {
        [Required,
        DisplayName("Mahalle adı"),
        MaxLength(255, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!")]
        public string Name { get; set; }

        [Required]
        public int TownId { get; set; }
    }
}
