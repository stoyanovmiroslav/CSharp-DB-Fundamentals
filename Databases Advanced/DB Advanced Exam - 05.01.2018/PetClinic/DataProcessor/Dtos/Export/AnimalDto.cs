using System;
using System.Collections.Generic;
using System.Text;

namespace PetClinic.DataProcessor.Dtos.Export
{
    public class AnimalDto
    {
        public string OwnerName { get; set; }

        public string AnimalName { get; set; }

        public int Age { get; set; }

        public string SerialNumber { get; set; }

        public string RegisteredOn { get; set; }
    }
}