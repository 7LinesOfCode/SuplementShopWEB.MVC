using SuplementShopWEB.MVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Domain.Interface
{
    public interface ICustomerRepository
    {
        public IQueryable<Customer> GetAllCustomers();
        public Customer GetCustomerById(int id);
        public void DeleteCustomer(int id); 
        public int AddNewCustomer (Customer customer);
        public void UpdateCustomer(Customer customer);
    }
}
