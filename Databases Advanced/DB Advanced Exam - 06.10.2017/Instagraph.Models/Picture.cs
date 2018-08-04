using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Instagraph.Models
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Path { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Size { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}