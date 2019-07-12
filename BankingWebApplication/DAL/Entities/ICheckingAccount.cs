using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public interface ICheckingAccount: IAccount
    {



        List<IAccount> GetAllAccounts(uint id);
      

    }
}
