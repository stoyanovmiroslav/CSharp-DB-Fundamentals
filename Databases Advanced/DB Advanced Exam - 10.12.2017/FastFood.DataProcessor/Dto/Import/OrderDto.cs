﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace FastFood.DataProcessor.Dto.Import
{
    [XmlType("Order")]
    public class OrderDto
    {
        [Required]
        [XmlElement("Customer")]
        public string Customer { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        [XmlElement("Employee")]
        public string Employee { get; set; }

        [Required]
        [XmlElement("DateTime")]
        public string DateTime { get; set; }


        [XmlElement("Type")]          //default: ForHere!!!!!!!
        public string Type { get; set; }

        [XmlArray("Items")]
        public ItemOrderDto[] Items { get; set; }
    }
}