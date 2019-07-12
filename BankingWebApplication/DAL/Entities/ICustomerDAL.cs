using System.Collections.Generic;

namespace DAL.Entities
{
    public interface ICustomerDAL
    {

        void Register(Customer customer);
        List<Customer> GetAllCustomer();
        int GenerateCustomerId();




    }
}


