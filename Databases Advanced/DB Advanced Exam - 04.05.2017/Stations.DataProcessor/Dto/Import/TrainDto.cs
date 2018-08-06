using Stations.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stations.DataProcessor.Dto.Import
{
    public class TrainDto
    {
        [Required]
        [MaxLength(10)]
        public string TrainNumber { get; set; }     //unique

        public TrainType? Type { get; set; }

        public ICollection<TrainSeatDto> Seats { get; set; } = new List<TrainSeatDto>();
    }
}
