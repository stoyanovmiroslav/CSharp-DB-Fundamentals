using PetClinic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Dtos.Import
{
    [XmlType("Procedure")]
    public class ProcedureDto
    {
        [Required]
        [XmlElement("Vet")]
        public string VetName { get; set; }

        [Required]
        [XmlElement("Animal")]
        public string AnimalSerialNumber { get; set; }

        [XmlArray("AnimalAids")]
        public AnimalAidDto[] ProcedureAnimalAids { get; set; }

        [Required]
        [XmlElement("DateTime")]
        public string DateTime { get; set; }
    }
}