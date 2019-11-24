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
    [Table("Person_Donation")]
    public class Person_Donation : BaseEntity
    {
        [Required]
        public int DonationId { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public DateTime AddedDate { get; set; }


        [Required,
        DisplayName("Açıklama"),
        MaxLength(255, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!")]
        public string Description { get; set; }
    }
}
