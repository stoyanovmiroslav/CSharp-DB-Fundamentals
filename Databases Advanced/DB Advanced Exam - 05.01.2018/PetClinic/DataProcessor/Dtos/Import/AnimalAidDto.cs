using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Dtos.Import
{
    [XmlType("AnimalAid")]
    public class AnimalAidDto
    {
        [XmlElement("Name")]
        public string Name { get; set; }
    }
}