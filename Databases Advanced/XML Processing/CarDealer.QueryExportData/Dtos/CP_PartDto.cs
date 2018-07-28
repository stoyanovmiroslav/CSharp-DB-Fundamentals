using System.Xml.Serialization;

namespace CarDealer.QueryExportData.Dtos
{
    [XmlType("part")]
    public class CP_PartDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}