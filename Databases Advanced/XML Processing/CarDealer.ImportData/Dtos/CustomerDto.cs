using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.ImportData.Dtos
{
    [XmlType("customer")]
    public class CustomerDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("birth-date")]
        public DateTime BirthDate { get; set; }

        [XmlElement("is-young-driver")]
        public bool IsYoungDriver { get; set; }
    }
}
