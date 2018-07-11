using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using System;
using System.Linq;

namespace P01_BillsPaymentSystem
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using (BillsPaymentSystemContext context = new BillsPaymentSystemContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Seed(context);

                Console.Write("Enter UserId: ");
                int userId = int.Parse(Console.ReadLine());
                Console.Write("Ënter BillsAmount: ");
                decimal amount = decimal.Parse(Console.ReadLine());

                PayBills(userId, amount, context);
                PrintUserStatement(context, userId);
            }
        }

        private static void PayBills(int userId, decimal amount, BillsPaymentSystemContext context)
        {
            var userCreditCards = context.PaymentMethods
                              .Where(u => u.UserId == userId && u.Type == Data.Models.Type.CreditCard)
                              .Include(e => e.CreditCard)
                              .OrderBy(x => x.CreditCardId)
                              .ToList();

            var userBankAccounts = context.PaymentMethods
                             .Where(u => u.UserId == userId && u.Type == Data.Models.Type.BankAccount)
                             .Include(e => e.BankAccount)
                             .OrderBy(x => x.BankAccountId)
                             .ToList();

            using (var transaction = context.Database.BeginTransaction())
            {
                foreach (var account in userBankAccounts)
                {
                    if (amount == 0)
                    {
                        break;
                    }

                    decimal currentBalance = account.BankAccount.Balance;

                    if (currentBalance >= amount)
                    {
                        account.BankAccount.Withdraw(amount);
                        amount -= amount;
                    }
                    else
                    {
                        account.BankAccount.Withdraw(currentBalance);
                        amount -= currentBalance;
                    }
                }

                foreach (var card in userCreditCards)
                {
                    if (amount == 0)
                    {
                        break;
                    }

                    decimal limitLeft = card.CreditCard.LimitLeft;

                    if (limitLeft >= amount)
                    {
                        card.CreditCard.Withdraw(amount);
                        amount -= amount;
                    }
                    else
                    {
                        card.CreditCard.Withdraw(limitLeft);
                        amount -= limitLeft;
                    }
                }

                if (amount == 0)
                {
                    transaction.Commit();
                    context.SaveChanges();
                }
                else
                {
                    transaction.Rollback();
                }
            }
        }

        private static void PrintUserStatement(BillsPaymentSystemContext context, int userId)
        {
            var user = context.Users
                              .Where(u => u.UserId == userId)
                              .Select(x => new
                              {
                                  x.FirstName,
                                  x.LastName,
                                  BankAccounts = x.PaymentMethods.Select(p => new { p.BankAccount }).ToList(),
                                  CreditCards = x.PaymentMethods.Select(p => new { p.CreditCard }).ToList()
                              }).FirstOrDefault();

            if (user == null)
            {
                Console.WriteLine($"User with id {userId} not found!");
                Environment.Exit(0);
            }

            Console.WriteLine($"User: {user.FirstName} {user.LastName}");

            Console.WriteLine("Bank Accounts:");
            foreach (var b in user.BankAccounts.Where(x => x.BankAccount != null))
            {
                Console.WriteLine($"-- ID: {b.BankAccount.BankAccountId}");
                Console.WriteLine($"--- Balance: {b.BankAccount.Balance}");
                Console.WriteLine($"--- Bank: {b.BankAccount.BankName}");
                Console.WriteLine($"--- SWIFT: {b.BankAccount.SwiftCode}");
            }

            bool areThereAnyBankAccounts = user.BankAccounts.Any(x => x.BankAccount != null);

            if (!areThereAnyBankAccounts)
            {
                Console.WriteLine("No any bank accounts");
            }

            Console.WriteLine("Credit Cards:");
            foreach (var c in user.CreditCards.Where(x => x.CreditCard != null))
            {
                Console.WriteLine($"-- ID: {c.CreditCard.CreditCardId}");
                Console.WriteLine($"--- Limit: {c.CreditCard.Limit}");
                Console.WriteLine($"--- Money Owed: {c.CreditCard.MoneyOwed}");
                Console.WriteLine($"--- Limit Left: {c.CreditCard.LimitLeft}");
                Console.WriteLine($"--- Expiration Date: {c.CreditCard.ExpirationDate}");
            }

            bool areThereAnyCreditCards = user.CreditCards.Any(x => x.CreditCard != null);

            if (!areThereAnyCreditCards)
            {
                Console.WriteLine("No any credit cards");
            }
        }

        private static void Seed(BillsPaymentSystemContext context)
        {
            User[] users = new[]
            {
                new User { FirstName = "Georgi", LastName = "Hristov", Email = "g.hristov@gmail.com", Password = "pas1234" } ,
                new User { FirstName = "Mihail", LastName = "Radev", Email = "m.radev@gmail.com", Password = "mpas234" } ,
                new User { FirstName = "Ivan", LastName = "Mihailov", Email = "i.mihailov@gmail.com", Password = "mypas32" },
                new User { FirstName = "Guy", LastName = "Gilbert", Email = "g.gilber@gmail.com", Password = "gilber1234" }
            };

            CreditCard[] creditCards = new[]
            {
                new CreditCard(new DateTime(2018, 12, 10), 100, 2000),
                new CreditCard(new DateTime(2019, 03, 05), 500, 1500),
                new CreditCard(new DateTime(2020, 03, 05), 100, 800)
            };

            BankAccount[] bankAccounts = new[]
            {
                new BankAccount("UBB", "UBBSBSF", 1200),
                new BankAccount("Unicredit Bulbank", "UNCRBGSF", 2000),
                new BankAccount("First Investment Bank", "FINVBGSF", 1000)
            };

            PaymentMethod[] paymentMetods = new[]
            {
                new PaymentMethod { User = users[3], CreditCard = creditCards[2], Type = Data.Models.Type.CreditCard },
                new PaymentMethod { User = users[3], BankAccount = bankAccounts[1], Type = Data.Models.Type.BankAccount },
                new PaymentMethod { User = users[3], BankAccount = bankAccounts[2], Type = Data.Models.Type.BankAccount },
                new PaymentMethod { User = users[2], BankAccount = bankAccounts[0], Type = Data.Models.Type.BankAccount },
                new PaymentMethod { User = users[1], CreditCard = creditCards[1], Type = Data.Models.Type.BankAccount }
            };

            context.Users.AddRange(users);
            context.PaymentMethods.AddRange(paymentMetods);

            context.SaveChanges();
        }
    }
}