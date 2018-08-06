using System.ComponentModel.DataAnnotations;

namespace Stations.DataProcessor.Dto.Import
{
    public class TrainSeatDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }   // unique

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string Abbreviation { get; set; }  //unique

        [Required]
        [Range(0, int.MaxValue)]
        public int? Quantity { get; set; }
    }
}