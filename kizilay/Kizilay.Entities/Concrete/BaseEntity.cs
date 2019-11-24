using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Entities.Concrete
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; }
    }
}
