using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stations.Models
{
    public class SeatingClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }   // unique

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string Abbreviation { get; set; }  //unique
    }
}