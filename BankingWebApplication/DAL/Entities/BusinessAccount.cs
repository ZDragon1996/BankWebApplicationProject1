using System;

using System.Collections.Generic;

namespace DAL.Entities
{
    public class BusinessAccount : Account, IBusinessAccount//must inherited account class before interfaces.
    {
        public BusinessAccount()
        {

        }


        //private properties
 

        private static List<IAccount> businessAccounts = new List<IAccount>();



        //methods
        public override void OpenAccount(IAccount account)
        {

            businessAccounts.Add(account);


        }//end method openaccount()

        public void CloseAccount()
        {
           
        }//end method CloseAccount()

        public override bool Deposit(IAccount account, decimal amount)
        {
            return base.Deposit(account, amount);
       
        }


        public override bool Withdraw(IAccount acc, decimal amount)
        {
            bool status = false;
        

                if (acc.AccountStatus)
                {
                    acc.Balance -= amount;
                    status = true;
                    if (!IsTransfer)
                    {
                        Transaction.CreateTransaction(acc, acc, amount, "Withdraw");
                    }
                }
                else
                {
                    Console.WriteLine("Record not found");
                }
            return status;

        }

        
        public override void Transfer(IAccount fromAccount, IAccount toAccount, decimal amount)
        {
            IsTransfer = true; // field for creating new transaction
            CheckingAccount ca = new CheckingAccount();
            BusinessAccount ba = new BusinessAccount();
            TermDeposit td = new TermDeposit();



            if (toAccount.AccountStatus)
            {
                if (toAccount.AccountType == "Loan")
                {
                    //loan type,  don't transfer
                }

                else if (toAccount.AccountType == "Term Deposit" && DateTime.Now < toAccount.MaturityDateTime)
                {
                    // account type is termdeposit, but maturity date greater than datime.now

                }

                else if (!fromAccount.AccountStatus || !toAccount.AccountStatus)
                {
                    Console.WriteLine($"One of the accounts is invalid transfer account type");
                }
                else if (fromAccount.Accountno == toAccount.Accountno)
                {
                    Console.WriteLine($"Can not transfer to the same acocunt!");
                }

                //checking type of  toAccount, from account is business account type.

                else if (toAccount.AccountType == "Checking")
                {
                    ba.Withdraw(fromAccount, amount);
                    ca.Deposit(toAccount, amount);
                }
                else if (toAccount.AccountType == "Business")
                {
                    ba.Withdraw(fromAccount, amount);
                    ba.Deposit(toAccount, amount);
                }
                else if (toAccount.AccountType == "Term Deposit")
                {
                    ba.Withdraw(fromAccount, amount);
                    td.Deposit(toAccount, amount);
                }

                else
                {
                    //should not get this far.
                }

            }//end if account is active
            else
            {
                //account is inactive
            }

        }

        public override void PayLoan(IAccount firstAccount, IAccount secondAccount, decimal amount)
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


            else if (secondAccount.AccountStatus)
            {
                firstAccount.Balance -= amount;
                secondAccount.Balance -= amount;
                Transaction.CreateTransaction(firstAccount, secondAccount, amount, "transfer");
            }
          

        }

        public List<IAccount> getAll()
        {


            foreach (var acc in businessAccounts)
            {
                Console.WriteLine($"Account Info: {acc}");
            }
            return businessAccounts;

        }

        public override string GetAccountType(IAccount account)
        {
            return AccountType = "Business";
        }

        public override double GetIntrestrate()
        {
            return Interestrate = 0.03;
        }

    }
}
