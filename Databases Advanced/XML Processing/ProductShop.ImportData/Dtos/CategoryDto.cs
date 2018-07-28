using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace ProductShopDatabase.Dtos
{
    [XmlType("category")]
    public class CategoryDto
    {
        [XmlElement("name")]
        public string Name { get; set; }
    }
}