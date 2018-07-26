using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusTicketsSystem.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public int TownId { get; set; }
        public Town HomeTowm { get; set; } //??

        public BankAccount BankAccounts { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}