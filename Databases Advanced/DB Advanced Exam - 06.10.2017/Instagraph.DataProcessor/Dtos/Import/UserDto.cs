using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Instagraph.DataProcessor.Dtos.Import
{
    public class UserDto
    {
        [MaxLength(30)]
        [Required]
        public string Username { get; set; }

        [MaxLength(20)]
        [Required]
        public string Password { get; set; }

        [Required]
        public string ProfilePicture { get; set; }
    }
}
