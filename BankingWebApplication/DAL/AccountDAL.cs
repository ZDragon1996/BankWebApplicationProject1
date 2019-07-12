using System.Collections.Generic;
using DAL.Entities;
using System;

namespace DAL
{
    public class AccountDAL: IAccountDAL
    {

      
        public AccountDAL()
        {
        }
        private static List<IAccount> accounts = new List<IAccount>();
  


        //public delegate void TransactionInfo(IAccount account, double amount);
        private static List<int> accountsno = new List<int>();
      




        #region List<IAccount> GetAllAccount()
        public List<IAccount> GetAllAccount()
        {
            //get all accounts without customerId
            return accounts;
        }
        #endregion 

        #region List<IAccount> GetAllAccount(int acc)
        public List<IAccount> GetAllAccount(int id)
        {

            List<IAccount> tempList = new List<IAccount>();
            foreach(var account in accounts)
            {
                if(account.CustomerId == id && account.AccountStatus)
                {
                    tempList.Add(account);
                }
            }

            return tempList;
        }
        #endregion

        #region List<IAccount> GetAllLoanAccounts(int customerId)
        public List<IAccount> GetAllLoanAccounts(int customerId)
        {
            List<IAccount> tempList = new List<IAccount>();
            foreach (var acc in accounts)
            {
                if (acc.CustomerId == customerId && acc.AccountStatus && acc.GetAccountType(acc) == "Loan")
                {
                    tempList.Add(acc);
                }
            }

            return tempList;
        }
        #endregion

        #region List<IAccount> GetAllAccountsWithoutLoan(int customerId)
        public List<IAccount> GetAllAccountsWithoutLoan(int customerId)
        {
            List<IAccount> tempList = new List<IAccount>();
            foreach (var acc in accounts)
            {
                if (acc.CustomerId == customerId && acc.AccountStatus && acc.GetAccountType(acc) != "Loan")
                {
                    tempList.Add(acc);
                }
            }

            return tempList;
        }
        #endregion

        #region void OpenAccount(IAccount account)
        public void OpenAccount(IAccount account, ApplicationDbContext _context)
        {
           
            _context.Add(account);
            _context.SaveChanges();


        }
        #endregion

        #region bool GetAccountStatus()
        public bool GetAccountStatus()
        {
            return true;
        }
        #endregion

        #region double GetBalance()
        public decimal GetBalance()
        {
            return 0M;
        }
        #endregion

        #region void CloseAccount(int accountno)
        public void CloseAccount(IAccount acc)
        {
            acc.CloseAccount(acc);

        }
        #endregion

        #region int GenerateAccountno()
        public int GenerateAccountno()
        {
       
            //int range: -2.147483648 x 10^9 to 2.147483647 x 10^9
            int intMax = int.MaxValue;

            Random random = new Random();
            int randomint = random.Next(1, intMax);

            while (accountsno.Contains(randomint)) //never ending loop if all numbers are taken
            {
                randomint = random.Next(1, intMax);
     
            }

            accountsno.Add(randomint);
            
            return randomint;
        }
        #endregion


        #region string GetAcountType(IAccount account)
        public string GetAcountType(IAccount account)
        {
            return account.AccountType;
        }
        #endregion

        #region GetIntrestrate(IAccount account)
        public double GetIntrestrate(IAccount account)
        {
            return account.GetIntrestrate();
        }
        #endregion

        #region int GetCustomerId(Customer customer)
        public int GetCustomerId(Customer customer)
        {
            return customer.CustomerId;
        }
        #endregion


        #region  void Deposit(IAccount account, decimal amount)
        public void  Deposit(IAccount acc, decimal amount, ApplicationDbContext _context)
        {
            if (acc.AccountStatus)
            {
                if (acc.AccountType == "Checking")
                {
                    CheckingAccount ca = new CheckingAccount();
                    ca.Deposit(acc, amount);

                }
                else if (acc.AccountType == "Business")
                {
                    BusinessAccount ba = new BusinessAccount();
                    ba.Deposit(acc, amount);
                }
                else if (acc.AccountType == "Loan")
                {
                   //no deposit to loan account
                }
                else if (acc.AccountType == "Term Deposit")
                {
                    TermDeposit td = new TermDeposit();
                    td.Deposit(acc, amount);
                }

                if(amount > 0 && acc.AccountType != "Term Deposit")
                {
                    //condition check to prevent negative amount transaction
                    Transaction transaction = CreateTransaction(acc, acc, amount, "Deposit", _context);
                    _context.Add(transaction);
                }else if(acc.MaturityDateTime <= DateTime.Now)
                {
                    Transaction transaction = CreateTransaction(acc, acc, amount, "Deposit", _context);
                    _context.Add(transaction);
                }
                

                _context.Update(acc);
                _context.SaveChanges();
            }//end if account is active
            else
            {
                //account is inactive
            }

        }
        #endregion


        #region void Withdraw(IAccount account,  decimal amount, ApplicationDbContext _context)
        public void Withdraw(IAccount account, decimal amount, ApplicationDbContext _context)
        {
            if (account.AccountStatus)
            {
                if (account.AccountType == "Checking")
                {
                    CheckingAccount ca = new CheckingAccount();
                    ca.Withdraw(account, amount);
                }
                else if (account.AccountType == "Business")
                {
                    BusinessAccount ba = new BusinessAccount();
                    ba.Withdraw(account, amount);
                }
                else if (account.AccountType == "Loan")
                {
                   // can not withdraw from loan
                }
                else if (account.AccountType == "Term Deposit")
                {
                    TermDeposit td = new TermDeposit(0);
                    td.Withdraw(account, amount);
                }

                if(amount > 0 && account.AccountType == "Business")
                {
                    Transaction transaction = CreateTransaction(account, account, amount, "Withdraw", _context);
                    _context.Add(transaction);
                }
                else if (amount > 0 && account.AccountType == "Term Deposit" && account.MaturityDateTime > DateTime.Now)
                {
                   
                }

                else if (amount > 0 && (account.Balance -amount) > 0)
                {
                    //condition check to prevent negative amount transaction
                    Transaction transaction = CreateTransaction(account, account, amount, "Withdraw", _context);
                    _context.Add(transaction);
                }
                else if (amount > 0 && (account.MaturityDateTime <= DateTime.Now))
                {
                    Transaction transaction = CreateTransaction(account, account, amount, "Withdraw", _context);
                    _context.Add(transaction);
                }


                _context.Update(account);
                _context.SaveChanges();

            }//end if account is active
            else
            {
                //account is not active
            }

        }
        #endregion

        #region bool AcccountIsFound(int accountno)
        public bool AcccountIsFound(int accountno)
        {
            IAccount account = new Account();
            return account.AcccountIsFound(accountno);

        }
        #endregion


        #region void Transfer(int fromAccountno, int toAccountno, decimal amount)
        public void Transfer(IAccount fromAccount, IAccount toAccount, decimal amount, ApplicationDbContext _context)
        {

            if (fromAccount.AccountStatus)
            {
                if (fromAccount.AccountType == "Business")
                {
                    Transaction transactionfromAccount = CreateTransaction(fromAccount, toAccount, amount, "Transfer", _context);
                    Transaction transactiontoAccount = CreateTransaction(fromAccount, toAccount, amount, "Received", _context);

                    _context.Add(transactionfromAccount);
                    _context.Add(transactiontoAccount);
                }
                else if (amount > 0 && fromAccount.Balance >= amount && fromAccount.AccountType != "Term Deposit")
                {
                    // aamount must greater than 0 to avoid unnecessary transaction
                    Transaction transactionfromAccount = CreateTransaction(fromAccount, toAccount, amount, "Transfer", _context);
                    Transaction transactiontoAccount = CreateTransaction(fromAccount, toAccount, amount, "Received", _context);

                    _context.Add(transactionfromAccount);
                    _context.Add(transactiontoAccount);
                }

                if (fromAccount.AccountType == "Checking")
                {
                    CheckingAccount ca = new CheckingAccount();
                    ca.Transfer(fromAccount, toAccount, amount);
                }
                else if (fromAccount.AccountType == "Business")
                {
                    BusinessAccount ba = new BusinessAccount();
                    ba.Transfer(fromAccount, toAccount, amount);
                }
                else if (fromAccount.AccountType == "Loan")
                {
                    //can not transfer to loan
                }
                else if (fromAccount.AccountType == "Term Deposit" )
                {
                    TermDeposit td = new TermDeposit();
                    td.Transfer(fromAccount, toAccount, amount);
                }

               
     
               
                _context.Update(fromAccount);
                _context.Update(toAccount);

                _context.SaveChanges();

            }//end if account is active
            else
            {
                //account is not active
            }

        }//end
        #endregion

        #region void PayLoan(int accountno, decimal amount)
        public void PayLoan(IAccount firstAccount, IAccount secondAccount, decimal amount, ApplicationDbContext _context)
        {
           
            if(secondAccount.AccountType == "Term Deposit")
            {
                TermDeposit td = new TermDeposit();
                td.PayLoan(firstAccount, secondAccount,amount);
            }
            else if(secondAccount.AccountType == "Business")
            {
                BusinessAccount ba = new BusinessAccount();
                ba.PayLoan(firstAccount,secondAccount, amount);
            }
            else if(secondAccount.AccountType == "Checking")
            {
                secondAccount.PayLoan(firstAccount, secondAccount, amount);
            }
            else if(secondAccount.AccountType == "Loan")
            {
                //loan can no pay loan
            }

            if (secondAccount.AccountType == "Business")
            {
                Transaction transaction = CreateTransaction(firstAccount, secondAccount, amount, "PayLoan", _context); //create transaction for loan account
                Transaction transactionfrom = CreateTransaction(secondAccount, firstAccount, amount, "Received", _context); //create transaction for from account

                _context.Add(transaction);
                _context.Add(transactionfrom);
            }

            else if (amount > 0 && (secondAccount.Balance - amount) >= 0 )
            {
                Transaction transaction = CreateTransaction(firstAccount, secondAccount, amount, "PayLoan", _context); //create transaction for loan account
                Transaction transactionfrom = CreateTransaction(secondAccount,firstAccount, amount, "Received", _context); //create transaction for from account

                _context.Add(transaction);
                _context.Add(transactionfrom);

            }
           

                _context.Update(firstAccount);
                _context.Update(secondAccount);
                _context.SaveChanges();
           
        }
        #endregion

         
        #region IAccount GetAccount(int accountno)
        public static IAccount GetAccount(int accountno, ApplicationDbContext _context)
        {

            IAccount ca = new CheckingAccount();
            IAccount bc = new BusinessAccount();
            IAccount la = new Loan();
            IAccount td = new TermDeposit();


            Account acc = _context.Account.Find(accountno);

            if(acc.AccountType == "Checking")
            {
                ca = acc;
                return ca;
            }
            else if (acc.AccountType == "Business")
            {
                bc = acc;
                return bc;
            }
            else if(acc.AccountType == "Loan")
            {
                la = acc;
                return la;
            }
            else if (acc.AccountType == "Term Deposit")
            {
                td = acc;
                return td;
            }
            else
            {
                // failed!
            }
            return ca;

        }
        #endregion

        #region List<Transaction> GetTransaction(int accountno)

        public List<Transaction> GetTransaction(int accountno)
        {
            List<Transaction> tempTrans = new List<Transaction>();
            foreach(var tran in Transaction.transactions)
            {
                if(tran.Accountno == accountno)
                {
                    tempTrans.Add(tran);
                }
            }
            return tempTrans;
        }
        #endregion

        #region Transaction CreateTransaction(IAccount account, IAccount account2, decimal amount, string info, ApplicationDbContext _context)
        public Transaction CreateTransaction(IAccount account, IAccount account2, decimal amount, string info, ApplicationDbContext _context)
        { 
            return Transaction.CreateTransaction(account, account2, amount, info);
        }

        double IAccountDAL.GetBalance()
        {
            throw new NotImplementedException();
        }

        public void OpenAccount(IAccount account)
        {
            throw new NotImplementedException();
        }

        public void CloseAccount(int id, int accountno)
        {
            throw new NotImplementedException();
        }

        public void Deposit(IAccount account, double amount)
        {
            throw new NotImplementedException();
        }

        public void Withdraw(IAccount account, double amount)
        {
            throw new NotImplementedException();
        }

        public void Transfer(int fromAccountno, int toAccountno, double amount)
        {
            throw new NotImplementedException();
        }

        public IAccount GetAccount(int accountno)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
