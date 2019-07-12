                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class CheckingAccount : Account, ICheckingAccount//must inherited account class before interfaces.
    {
        public CheckingAccount()
        {

        }

      
        //private properties
        private static List<IAccount> checkingAccounts = new List<IAccount>();


        public override void PayLoan(IAccount firstAccount, IAccount secondAccount, decimal amount)
        {
            base.PayLoan(firstAccount, secondAccount, amount);
        }

        public override void OpenAccount(IAccount account)
        {
            checkingAccounts.Add(account);
        }

        public override bool Deposit(IAccount account, decimal amount)
        {
            return base.Deposit(account, amount);
        }


        public override bool Withdraw(IAccount account, decimal amount)
        {

           return base.Withdraw(account, amount);
               
         }
        public override void Transfer(IAccount fromAccount, IAccount toAccount, decimal amount)
        {

            if (toAccount.AccountStatus)
            {
                if (toAccount.AccountType == "Term Deposit" && DateTime.Now >= toAccount.MaturityDateTime)
                {
                    base.Transfer(fromAccount, toAccount, amount);
                }
                else if (toAccount.AccountType != "Term Deposit")
                {
                    base.Transfer(fromAccount, toAccount, amount);
                }

                else
                {
                    //loan or termdeposit type
                }
            }//end if account is active
            else
            {
                //account is inative
            }
           


        }



        public override string GetAccountType(IAccount account)
        {
            return AccountType = "Checking";
        }

        public override double GetIntrestrate()
        {
            return Interestrate = 0.02;
        }


        public List<IAccount> GetAllAccounts(uint id)
        {
            List<IAccount> tempList = new List<IAccount>();

            foreach(var acc in checkingAccounts)
            {
                if(acc.CustomerId == id)
                {
                    tempList.Add(acc);
                }
            }
            return tempList;
        }


 

    }

}
