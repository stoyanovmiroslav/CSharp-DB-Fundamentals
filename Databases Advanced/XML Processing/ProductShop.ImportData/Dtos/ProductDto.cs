using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace ProductShopDatabase.Dtos
{
    [XmlType("product")]
    public class ProductDto
    {
        [MinLength(3)]
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("buyer")]
        public UserDto Buyer { get; set; }

        [XmlElement("seller")]
        public UserDto Seller { get; set; }
    }
}
