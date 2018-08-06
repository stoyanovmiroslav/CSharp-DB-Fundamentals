using System.Xml.Serialization;

namespace Stations.DataProcessor.Dto.Import
{
    [XmlType("Card")]
    public class CardDto
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
    }
}