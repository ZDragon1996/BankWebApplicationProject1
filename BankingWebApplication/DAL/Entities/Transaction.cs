using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Transaction
    {
        public Transaction()
        {

        }

        [Key]
        public int TransactionNo { get; set; }
       
        public int Accountno { get; set; }
        public int CustomerId { get; set; }
        public string TransactionInfo { get; set; }

        public DateTime Time { get; set; }
       
       
        public static List<Transaction> transactions = new List<Transaction>();


        static public int GenerateTransactionNo()
        {
            //uint range: 4.294967295 × 10^9
            //int range: -2.147483648 x 10^9 to 2.147483647 x 10^9
            //uint uintMax = uint.MaxValue;

            int intMax = int.MaxValue;

            Random random = new Random();
            int randomint = random.Next(1, intMax);


            Console.WriteLine($"--->>>Generate random Transaction No: {randomint}<<<---");
            return randomint;
        }

        public static Transaction CreateTransaction(IAccount account, IAccount account2, decimal amount, string type)
        {
            string transactionInfo;
            int accountno = account.Accountno;
            int customerId = account.CustomerId;

            Transaction transaction = new Transaction()
            {
                TransactionNo = GenerateTransactionNo(),
                Accountno = accountno,
                CustomerId = customerId,
                
                Time = DateTime.Now
            };

            switch (type)
            {
                case "Deposit":
                    transactionInfo = $"Deposit {amount} to ({account.AccountType}){account.Accountno}";
                    transaction.TransactionInfo = transactionInfo;
                    break;
                case "Withdraw":
                    transactionInfo = $"WIthdraw {amount} from ({account.AccountType}){account.Accountno}";
                    transaction.TransactionInfo = transactionInfo;
                    break;

                case "Transfer":              
                    transactionInfo = $"Transfer {amount} to ({account2.AccountType}){account2.Accountno}";
                 
                    transaction.TransactionInfo = transactionInfo;
                    break;
                case "Received":
                    transactionInfo = $"Received {amount} from ({account.AccountType}){account.Accountno}";
                    transaction.TransactionInfo = transactionInfo;
                    transaction.Accountno = account2.Accountno;
                    break;
                case "PayLoan":
                    transactionInfo = $"Pay {amount} from: {account2.Accountno}";
                    transaction.Accountno = account2.Accountno;
                    transaction.TransactionInfo = transactionInfo;
                    break;
                default:
                    transactionInfo = $"Transaction type is invalid";
                    transaction.TransactionInfo = transactionInfo;
                    break;

            }

          

            return transaction;

        }

    }
}
