using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Dtos.Export
{
    [XmlType("AnimalAid")]
    public class AnimalAidDto
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Price")]
        public decimal Price { get; set; }
    }
}