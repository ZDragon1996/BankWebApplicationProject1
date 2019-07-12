using System;
using System.Collections.Generic;
namespace DAL.Entities
{ 
    public class TermDeposit: Account, ITermDeposit
    {
        public TermDeposit(decimal termdepositamount)
        {
            Balance = termdepositamount;

        }

        public TermDeposit()
        {

        }

    
        private List<IAccount> termDepositAccounts = new List<IAccount>();

        DateTime maturityDateTime = new DateTime(2020,01,01); // add two year from currentdatetime
        DateTime currentDateTime = DateTime.Now;


        //public override void OpenAccount(IAccount account)
        //{
        //    termDepositAccounts.Add(account);

        //}//end method OpenAccount()


        public override string GetAccountType(IAccount account)
        {
            return AccountType = "Term Deposit";
        }

        public override bool Deposit(IAccount acc, decimal amount)
        {

            bool status = false;
            if (acc.AccountStatus)
            {
                if (acc.Balance >= amount && acc.MaturityDateTime < currentDateTime)
                {
                    acc.Balance += amount;
                    status = true;
                }
                else
                {
                    Console.WriteLine($"You can not withdraw or transfer after {maturityDateTime}");
                }

            }

            else
            {
                Console.WriteLine("Record not found");
            }

            return status;

        }

        public override bool Withdraw(IAccount acc, decimal amount)
        {
            bool status = false;
  

            if (acc.AccountStatus)
                {
                if (acc.Balance >= amount && acc.MaturityDateTime < currentDateTime)
                {
                        acc.Balance -= amount;
                        status = true;
                    }
                    else
                    {
                        Console.WriteLine($"You can not withdraw or transfer after {maturityDateTime}");
                    }

                }

                else
                {
                    Console.WriteLine("Record not found");
                }
               


            return status;

        } // end Withdraw

        public override void Transfer(IAccount fromAccount, IAccount toAccount, decimal amount)
        {
            if (toAccount.AccountStatus)
            {
                if (fromAccount.MaturityDateTime <= DateTime.Now)
                {
                    base.Transfer(fromAccount, toAccount, amount);
                }
                else
                {
                    Console.WriteLine($"Can not transfer before {maturityDateTime}");
                }

            }//end if account is active
            else
            {
                //acocunt is inactive
            }

        }

        public override void PayLoan(IAccount fromAccount, IAccount toAccount, decimal amount)
        {
            if (toAccount.AccountStatus)
            {
                if (toAccount.MaturityDateTime <= DateTime.Now)
                {
                    base.PayLoan(fromAccount, toAccount, amount);
                }
                else
                {
                    Console.WriteLine($"Can not transfer before {maturityDateTime}");
                }

            }//end if account is active
            else
            {
                //acocunt is inactive
            }

        }


        public override double GetIntrestrate()
        {
            return Interestrate = 0.01;
        }

    }
}
