using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class BankAccount
    {
        public BankAccount(string bankName, string swiftCode, decimal balance)
        {
            this.BankName = bankName;
            this.SwiftCode = swiftCode;
            this.Deposit(balance);
        }

        [Key]
        public int BankAccountId { get; set; }

        [NotMapped]
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public decimal Balance { get; private set; }

        [Required]
        [MaxLength(50)]
        public string BankName { get; set; }

        [Required]
        [MaxLength(20)]
        public string SwiftCode { get; set; }


        public void Withdraw(decimal amount)
        {
            if (amount <= 0 || this.Balance - amount < 0)
            {
                throw new ArgumentException("Invalid Operation");
            }
            this.Balance -= amount;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Invalid Operation");
            }
            this.Balance += amount;
        }
    }
}