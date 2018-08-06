using Stations.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stations.DataProcessor.Dto.Import
{
    public class TripDto
    {
        [Required]
        public string Train { get; set; }

        [Required]
        public string OriginStation { get; set; }

        [Required]
        public string DestinationStation { get; set; }

        [Required]
        public DateTime? DepartureTime { get; set; }

        [Required]
        public DateTime? ArrivalTime { get; set; }  //must be after departure time

        public TripStatus Status { get; set; } = TripStatus.OnTime;

        public TimeSpan? TimeDifference { get; set; }  //time(span) representing how late or early a given train was


    }
}
