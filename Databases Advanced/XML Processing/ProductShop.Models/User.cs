using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductShop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        [MinLength(3)]
        [Required]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public ICollection<Product> ProductsBuyer { get; set; }

        public ICollection<Product> ProductsSeler { get; set; }
    }
}
