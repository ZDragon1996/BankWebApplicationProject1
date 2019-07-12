using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Account : IAccount
    {


        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Amount can not be negative")]
        public decimal Amount;

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Balance { get; set; }

        [Key]
        public int Accountno { get; set; }
       
        public string AccountType { get; set; }
        public int CustomerId { get; set; }

        public bool AccountStatus { get; set; }

        public Customer customer { get; set; }

        //public List<Transaction> transactions { get; set; }

        public DateTime? MaturityDateTime { get; set; }

        public bool IsTransfer;


        protected static List<IAccount> accounts = new List<IAccount>();

        
        public double Interestrate { get; set; }

        public int FromAccountno;

        public int ToAccountno;


        public Account()
        {
            AccountStatus = true;
       
        }

        public virtual void OpenAccount(IAccount account)
        {
            Console.WriteLine("Base OpenAccount");
            accounts.Add(account);



        }

        public virtual void CloseAccount(IAccount acc)
        {

            IAccount account = GetAccountByAccountno(acc.Accountno);

            if (AcccountIsFound(acc.Accountno))
            {
                if (account.AccountStatus && account.Balance >= 0)
                {
                    account.AccountStatus = false;

                    Console.WriteLine($"Succeed!! Close account no : {account.Accountno}");
                }
                else if (account.AccountStatus)
                {
                    Console.WriteLine($"Failed!! Balance is {account.Balance}");
                }
            }
            else
            {
                Console.WriteLine($"Can not find record");
            }
        }

        public virtual double GetIntrestrate()
        {
            return Interestrate;
        }



        public virtual string GetAccountType()
        {
            return AccountType;
        }

        public decimal GetBalance()
        {
            return Balance;
        }

        #region bool AcccountIsFound(int accountno)
        public bool AcccountIsFound(int accountno)
        {
            bool status = false;
            foreach (var acc in accounts)
            {
                if (acc.Accountno == accountno && acc.AccountStatus)
                {
                    status = true;
                }
            }
            return status;

        }
        #endregion


        public virtual bool Withdraw(IAccount acc, decimal amount)
        {
            bool status = false;
           

            if ((acc.Balance - amount) >= 0 && acc.AccountStatus)
            {
                if (!IsTransfer)
                {
                    //Transaction.CreateTransaction(account, account, amount, "Withdraw");
                }
                acc.Balance -= amount;
                status = true;

            }
            else if (acc.AccountStatus)
            {
                Console.WriteLine("Insufficient fund, Please try again!");
            }
            return status;

        }

       
        public virtual bool Deposit(IAccount acc, decimal amount)
        {
            bool status = false;
       
            if (acc.AccountStatus && amount > 0)
            {
                acc.Balance += amount;
                if (!IsTransfer)
                {
                    //Transaction.CreateTransaction(acc, acc, amount, "Deposit");
                }
                status = true;
            }
            else
            {
                Console.WriteLine("Faield, amount is possibly less or equal to 0");
            }
            return status;
        }


        public virtual void Transfer(IAccount firstAccount, IAccount secondAccount, decimal amount)
        {
            IsTransfer = true;

          
            if(!firstAccount.AccountStatus || !secondAccount.AccountStatus)
            {
                Console.WriteLine($"One of the Accounts is invalid");
            }
            else if (firstAccount.Accountno == secondAccount.Accountno)
            {
                Console.WriteLine($"Can not transfer to the same acocunt!");
            }

            else if (firstAccount.Balance >= amount && secondAccount.Deposit(secondAccount, amount))
            {
                firstAccount.Withdraw(firstAccount, amount);
               
            }
            else if (firstAccount.Balance >= amount)
            {
                Console.WriteLine($"Invalid transfer account type");
            }
            else
            {
                Console.WriteLine($"Sorry!! Insufficient fund, Missing ${amount - firstAccount.Balance}");
            }
     
            
        }

       



        public virtual string GetAccountType(IAccount account)
        {
            Console.WriteLine("Base GetAccountType");
            return "Base of Account";
        }


       

        public IAccount GetAccountByAccountno(int accno)
        {
            IAccount tempAc = new Account();
            foreach(var acc in accounts)
            {
                if(acc.Accountno == accno)
                {
                    tempAc = acc;
                }
            }
            return tempAc;
        }

        public IAccount GetAccount(int accoutno)
        {
            IAccount tempAcc = new Account();
            foreach(var acc in accounts)
            {
                if (acc.Accountno == accoutno)
                {
                    tempAcc = acc;
                }
            }
            return tempAcc;
        }

        public virtual void PayLoan(IAccount firstAccount, IAccount secondAccount, decimal amount)
        {
            IsTransfer = true;


            if (!firstAccount.AccountStatus || !secondAccount.AccountStatus)
            {
                Console.WriteLine($"One of the Accounts is invalid");
            }
            else if (firstAccount.Accountno == secondAccount.Accountno)
            {
                Console.WriteLine($"Can not transfer to the same acocunt!");
            }


            else if (secondAccount.Balance >= amount)
            {
                firstAccount.Balance -= amount;
                secondAccount.Balance -= amount;
               
            
            }
            else if (firstAccount.Balance >= amount)
            {
                Console.WriteLine($"Invalid transfer account type");
            }
            else
            {
                Console.WriteLine($"Sorry!! Insufficient fund, Missing ${amount - firstAccount.Balance}");
            }
    
        }

    }
}
