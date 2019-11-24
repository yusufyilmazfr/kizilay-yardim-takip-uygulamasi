using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Entities.Concrete
{
    public class Admin : BaseEntity
    {
        [Required,
        DisplayName("Kullanıcı adı"),
        MaxLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!")]
        public string Username { get; set; }

        [Required,
        DisplayName("Şifre"),
        MaxLength(32, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!")]
        public string Passwd { get; set; }
    }
}
