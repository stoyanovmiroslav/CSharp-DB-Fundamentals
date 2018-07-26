using System;
using System.Collections.Generic;

namespace BusTicketsSystem.Models
{
    public class BusCompany
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Nationality { get; set; }

        public int Rating { get; set; }

        public ICollection<Trip> Trips { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
