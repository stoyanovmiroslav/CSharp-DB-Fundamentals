﻿using System;
using System.Collections.Generic;

namespace _02._Database_First.Data.Models
{
    public class Address
    {
        public Address()
        {
            Employees = new HashSet<Employee>();
        }

        public int AddressId { get; set; }
        public string AddressText { get; set; }
        public int? TownId { get; set; }

        public Town Town { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
