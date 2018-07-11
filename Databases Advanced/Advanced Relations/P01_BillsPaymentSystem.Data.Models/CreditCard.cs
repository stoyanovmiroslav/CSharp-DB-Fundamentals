using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class CreditCard
    {
        public CreditCard(DateTime expirationDate, decimal moneyOwed, decimal limit)
        {
            this.ExpirationDate = expirationDate;
            this.MoneyOwed = moneyOwed;
            this.Limit = limit;
        }

        [Key]
        public int CreditCardId { get; set; }

        [NotMapped]
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public decimal Limit { get; private set; }

        [Required]
        public decimal MoneyOwed { get; set; }

        [NotMapped]
        public decimal LimitLeft => this.Limit - this.MoneyOwed;

        [Required]
        public DateTime ExpirationDate { get; set; }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0 || this.MoneyOwed + amount > this.Limit)
            {
                throw new ArgumentException("Invalid Operation");
            }
            this.MoneyOwed += amount;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0 || this.MoneyOwed - amount < 0)
            {
                throw new ArgumentException("Invalid Operation");
            }
            this.MoneyOwed -= amount;
        }

        public void ChangeCreditCardLimit(decimal newLimit)
        {
            this.Limit = newLimit;
        }


        //has a unique combination of UserId, BankAccountId and CreditCardId!
    }
}
