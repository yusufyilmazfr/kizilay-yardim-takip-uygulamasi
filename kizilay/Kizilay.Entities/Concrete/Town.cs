using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Entities.Concrete
{
    [Table("Towns")]
    public class Town : BaseEntity
    {
        [Required,
        DisplayName("Şehir"),
        MaxLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!")]
        public string Name { get; set; }

        [Required]
        public int CityId { get; set; }
    }
}
