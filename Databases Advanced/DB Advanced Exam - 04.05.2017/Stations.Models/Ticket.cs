using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stations.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(8)]
        public string SeatingPlace { get; set; }  //which combines seating class abbreviation plus a positive integer

        [Required]
        public int TripId { get; set; }
        [Required]
        public Trip Trip { get; set; }

        public int? CustomerCardId { get; set; }
        public CustomerCard CustomerCard { get; set; }
    }
}