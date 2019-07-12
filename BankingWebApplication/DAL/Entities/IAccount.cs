﻿using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public interface IAccount
    {
        
         //properties
        //List<Account> getAllAccount(uint id);
        string AccountType { get; set; }
        int Accountno { get; set; }

        decimal Balance { get; set; }
        bool AccountStatus { get; set; }

        int CustomerId { get; set; }
        //Customer customer { get; set; } // account belong to 1 customer
        double Interestrate { get; set; }

        DateTime? MaturityDateTime { get; set; }

        Customer customer { get; set; }

     


        //List<Transaction> transactions { get; set; }


        //methods
        void OpenAccount(IAccount account);
        void CloseAccount(IAccount account);
        string GetAccountType(IAccount account);
        bool Withdraw(IAccount account, decimal amount);
        bool Deposit(IAccount account, decimal amount);
        void Transfer(IAccount fromAccount, IAccount toAccount, decimal amount);
        bool AcccountIsFound(int accountno);
        IAccount GetAccount(int accountno);
        double GetIntrestrate();

        void PayLoan(IAccount firstAccount, IAccount secondAccount, decimal amount);


    }
}
