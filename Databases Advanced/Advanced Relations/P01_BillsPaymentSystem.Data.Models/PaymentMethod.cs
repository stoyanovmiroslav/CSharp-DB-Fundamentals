using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Type Type { get; set; }

        [Required]
        public User User { get; set; }
        public int UserId { get; set; }

        public BankAccount BankAccount { get; set; }
        public int? BankAccountId { get; set; }

        public CreditCard CreditCard { get; set; }
        public int? CreditCardId { get; set; }

        //always one of them is null and the other one is not (add a CHECK constraint)
    }
}