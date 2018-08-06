using Stations.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Stations.DataProcessor.Dto.Import
{
    [XmlType("Card")]
    public class CustomerCardDto
    {
        [Required]
        [MaxLength(128)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Range(0, 120)]
        [XmlElement("Age")]
        public int Age { get; set; }

        [XmlElement("CardType")]
        public CardType Type { get; set; } = CardType.Normal;
    }
}
