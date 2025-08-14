using System;
using System.Collections.Generic;

namespace Q1_FinanceManagementSystem
{
    // a) Record for Transaction
    public record Transaction(int Id, DateTime Date, decimal Amount, string Category);

    // b) Interface for transaction processing
    public interface ITransactionProcessor
    {
        void Process(Transaction transaction);
    }

    // c) Concrete processors
    public class BankTransferProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"[Bank Transfer] Processing {transaction.Amount:C} for {transaction.Category} on {transaction.Date.ToShortDateString()}");
        }
    }

    public class MobileMoneyProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"[Mobile Money] Sending {transaction.Amount:C} for {transaction.Category} on {transaction.Date.ToShortDateString()}");
        }
    }

    public class CryptoWalletProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"[Crypto Wallet] Transferring {transaction.Amount:C} for {transaction.Category} on {transaction.Date.ToShortDateString()}");
        }
    }

    // d) Base Account class
    public class Account
    {
        public string AccountNumber { get; }
        public decimal Balance { get; protected set; }

        public Account(string accountNumber, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }

        public virtual void ApplyTransaction(Transaction transaction)
        {
            Balance -= transaction.Amount;
        }
    }

    // e) Sealed SavingsAccount class
    public sealed class SavingsAccount : Account
    {
        public SavingsAccount(string accountNumber, decimal initialBalance)
            : base(accountNumber, initialBalance)
        {
        }

        public override void ApplyTransaction(Transaction transaction)
        {
            if (transaction.Amount > Balance)
            {
                Console.WriteLine("Insufficient funds");
            }
            else
            {
                Balance -= transaction.Amount;
                Console.WriteLine($"Transaction applied. New balance: {Balance:C}");
            }
        }
    }

    // f) FinanceApp
    public class FinanceApp
    {
        private List<Transaction> _transactions = new();

        public void Run()
        {
            var account = new SavingsAccount("SA-001", 1000m);

            var t1 = new Transaction(1, DateTime.Now, 150m, "Groceries");
            var t2 = new Transaction(2, DateTime.Now, 300m, "Utilities");
            var t3 = new Transaction(3, DateTime.Now, 50m, "Entertainment");

            var mobileProcessor = new MobileMoneyProcessor();
            var bankProcessor = new BankTransferProcessor();
            var cryptoProcessor = new CryptoWalletProcessor();

            mobileProcessor.Process(t1);
            account.ApplyTransaction(t1);

            bankProcessor.Process(t2);
            account.ApplyTransaction(t2);

            cryptoProcessor.Process(t3);
            account.ApplyTransaction(t3);

            _transactions.AddRange(new[] { t1, t2, t3 });
        }
    }

    // Main entry point
    class Program
    {
        static void Main(string[] args)
        {
            var app = new FinanceApp();
            app.Run();
        }
    }
}
