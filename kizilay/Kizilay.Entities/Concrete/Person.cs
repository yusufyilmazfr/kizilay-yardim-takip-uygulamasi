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
    [Table("Person")]
    public class Person : BaseEntity
    {
        [Required,
        DisplayName("T.C No"),
        StringLength(11, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!")]
        public string TC { get; set; }

        [Required,
        DisplayName("Vatandaşlık"),
        MaxLength(5, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!")]
        public string Citizenship { get; set; }

        [Required,
        DisplayName("Ad"),
        MaxLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!")]
        public string Name { get; set; }

        [Required,
        DisplayName("Soyad"),
        MaxLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!")]
        public string Surname { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required,
        DisplayName("Doğum yeri"),
        MaxLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!")]
        public string PlaceOfBirth { get; set; }

        public string Reference { get; set; }

        public bool Gender { get; set; }

        public bool JobState { get; set; }

        [DisplayName("İş açıklaması")]
        public string JobDescription { get; set; }

        [DisplayName("Maaş")]
        public decimal Salary { get; set; }

        [DisplayName("Ev Telefonu"),
        MaxLength(20, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!")]
        public string HomePhone { get; set; }

        [DisplayName("Cep Telefonu"),
        MaxLength(20, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!")]
        public string MobilePhone { get; set; }

        [Required,
        DisplayName("Anne adı"),
        MaxLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!")]
        public string MotherName { get; set; }

        [Required,
        DisplayName("Baba adı"),
        MaxLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!")]
        public string FatherName { get; set; }

        public string isMarried { get; set; }

        [Required]
        public int EducationalStatus { get; set; }
        [Required]
        public int SocialSecurityId { get; set; }
        [Required]
        public int FamilyId { get; set; }
    }
}
