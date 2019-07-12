using System;
using DAL.Entities;
using DAL;
using System.Collections.Generic;


namespace BusinessLayer
{
    public class CustomerBL
    {

        

        CustomerDAL customerdal = new CustomerDAL();
     

        public CustomerBL()
        {

        }

        public  void Register(Customer customer , ApplicationDbContext _context)
        {
             customerdal.Register(customer,_context);
       
        }

        public List<Customer> GetAllCustomer()
        {
            return customerdal.GetAllCustomer();
        }

        public int GenerateCustomerId()
        {
            return customerdal.GenerateCustomerId();
        }

      

    }
}
