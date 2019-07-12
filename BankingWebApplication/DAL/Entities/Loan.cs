using System;
namespace DAL.Entities
{
    public class Loan : Account, ILoan
    {



        public Loan(decimal amount)
        {
            Balance = amount;
        }

        public Loan()
        {

        }

        public override bool Deposit(IAccount account, decimal amount)
        {
            Console.WriteLine("Can not deposit to Loan Account");
            return false;
        }

        public override bool Withdraw(IAccount account, decimal amount)
        {
            Console.WriteLine("Can not withdraw from Loan Account");
            return false;
        }

        public override void Transfer(IAccount fromAccount, IAccount toAccount, decimal amount)
        {
            Console.WriteLine("Can not transfter from Loan Account");
        }

        public override double GetIntrestrate()
        {
            return Interestrate = 0.08;
        }

        public override string GetAccountType(IAccount account)
        {
            return AccountType = "Loan";
        }
     

        public override void PayLoan(IAccount fromAccount, IAccount toAcocunt,decimal amount)
        {

    
           //loan can not pay loan
        }

 

    }
}
