using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.ImportData.Dtos
{
    [XmlType("supplier")]
    public class SupplierDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("is-importer")]
        public bool IsImporter { get; set; }
    }
}
