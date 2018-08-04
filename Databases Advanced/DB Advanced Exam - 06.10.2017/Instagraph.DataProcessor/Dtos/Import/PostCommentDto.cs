using System.Xml.Serialization;

namespace Instagraph.DataProcessor.Dtos.Import
{
    [XmlType("post")]
    public class PostCommentDto
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
    }
}