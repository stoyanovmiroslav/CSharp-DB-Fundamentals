using System;
using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class Passport
    {
        [Key]
        [RegularExpression(@"^[a-zA-Z]{7}\d{3}$")]
        public string SerialNumber { get; set; }

        public Animal Animal { get; set; }

        [RegularExpression(@"^(?:\+359|0)\d{9}$")]
        public string OwnerPhoneNumber { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string OwnerName { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }
    }
}