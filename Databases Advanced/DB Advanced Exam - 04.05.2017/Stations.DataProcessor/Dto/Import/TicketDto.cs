using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Stations.DataProcessor.Dto.Import
{
    [XmlType("Ticket")]
    public class TicketDto
    {
        [XmlAttribute("price")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [XmlAttribute("seat")]
        [MaxLength(8)]
        public string Seat { get; set; }

        [XmlElement("Trip")]
        public TripTicketDto Trip { get; set; }

        [XmlElement("Card")]
        public CardDto Card { get; set; }
    }
}