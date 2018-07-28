using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.QueryExportData.Dtos
{
    [XmlType("sold-products")]
    public class UP_SoldProductDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("product")]
        public UP_ProductDto[] Product { get; set; }
    }
}
