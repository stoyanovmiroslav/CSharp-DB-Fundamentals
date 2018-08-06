using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stations.DataProcessor.Dto.Import
{
    public class StationDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }        // unique

        [MaxLength(50)]
        public string Town { get; set; }
    }
}
