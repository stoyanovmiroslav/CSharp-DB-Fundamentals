using System.Collections.Generic;

namespace BusTicketsSystem.Models
{
    public class BusStation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TownId { get; set; }
        public Town Town { get; set; }

        public ICollection<Trip> TripsOrigin { get; set; }

        public ICollection<Trip> TripsDest { get; set; }
    }
}