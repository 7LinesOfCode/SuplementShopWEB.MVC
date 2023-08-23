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

            var customerToDelete = _context
                        .Customers
                        .Find(id);

            if(customerToDelete != null)
            {
                _context.Customers.Remove(customerToDelete);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException($"Customer with {id} doesn't exist");
            }
        }

        public int AddNewCustomer(Customer customer)
        {
           _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer.Id;
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Customers.Attach(customer);
            _context.Entry(customer).Property("FirstName").IsModified = true;
            _context.Entry(customer).Property("LastName").IsModified = true;
            _context.Entry(customer).Property("Address").IsModified = true;
            _context.Entry(customer).Property("Country").IsModified = true;
            _context.Entry(customer).Property("PhoneNumber").IsModified = true;
            _context.Entry(customer).Property("EmailAddress").IsModified = true;

            _context.SaveChanges();
        }
    }
}
