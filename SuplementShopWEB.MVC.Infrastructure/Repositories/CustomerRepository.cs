using SuplementShopWEB.MVC.Domain.Interface;
using SuplementShopWEB.MVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly Context _context;
        public CustomerRepository(Context context)
        {
            _context= context;
        }

        public IQueryable<Customer> GetAllCustomers()
        {
            return _context.Customers;  
        }

        public Customer GetCustomerById(int id)
        {
            return _context.Customers.SingleOrDefault(i => i.Id == id);
        }

        public void DeleteCustomer (int id)
        {
            if(id == null)
            {
                throw new ArgumentNullException($"Customer with {id} doesn't exist");
            }
            else
            {
                var customerToDelete = _context.Customers
                    .FirstOrDefault(i => i.Id == id);
                _context.Remove(customerToDelete);
                _context.SaveChanges();
            }
        }

        public int AddNewCustomer(Customer customer)
        {
           _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer.Id;
        }
    }
}
