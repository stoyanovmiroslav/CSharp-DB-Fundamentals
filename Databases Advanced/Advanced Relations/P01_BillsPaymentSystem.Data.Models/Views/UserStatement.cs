using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;

namespace P01_BillsPaymentSystem.Data.Models.Views
{
    public class UserStatement
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<BankAccount> BankAccounts { get; set; }

        public List<CreditCard> CreditCards { get; set; }


        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"User: {this.FirstName} {this.LastName}");

            stringBuilder.AppendLine("Bank Accounts:");
            foreach (var b in this.BankAccounts.Where(x => x != null))
            {
                stringBuilder.AppendLine($"-- ID: {b.BankAccountId}");
                stringBuilder.AppendLine($"--- Balance: {b.Balance}");
                stringBuilder.AppendLine($"--- Bank: {b.BankName}");
                stringBuilder.AppendLine($"--- SWIFT: {b.SwiftCode}");
            }

            bool areThereAnyBankAccounts = this.BankAccounts.Any(x => x != null);

            if (!areThereAnyBankAccounts)
            {
                stringBuilder.AppendLine("No any bank accounts");
            }

            stringBuilder.AppendLine("Credit Cards:");
            foreach (var c in this.CreditCards.Where(x => x != null))
            {
                stringBuilder.AppendLine($"-- ID: {c.CreditCardId}");
                stringBuilder.AppendLine($"--- Limit: {c.Limit}");
                stringBuilder.AppendLine($"--- Money Owed: {c.MoneyOwed}");
                stringBuilder.AppendLine($"--- Limit Left: {c.LimitLeft}");
                stringBuilder.AppendLine($"--- Expiration Date: {c.ExpirationDate.ToString("yyyy/MM", CultureInfo.InvariantCulture)}");
            }

            bool areThereAnyCreditCards = this.CreditCards.Any(x => x != null);

            if (!areThereAnyCreditCards)
            {
                stringBuilder.AppendLine("No any credit cards");
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
