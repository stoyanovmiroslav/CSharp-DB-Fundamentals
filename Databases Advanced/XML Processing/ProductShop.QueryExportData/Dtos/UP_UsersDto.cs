using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.QueryExportData.Dtos
{
    [XmlType("users")]
    public class UP_UsersDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("user")]
        public UP_UserDto[] Users { get; set; }
    }
}
