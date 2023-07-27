using SuplementShopWEB.MVC.Application.Interfaces;
using SuplementShopWEB.MVC.Application.ViewModel.Customer;
using SuplementShopWEB.MVC.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;

        public CustomerService(ICustomerRepository repo)
        {
            _repo= repo;
        }


        public int AddCustomer(NewCustomerVm newCustomerModel)
        {
            throw new NotImplementedException();
        }

        public int AddCustomer()
        {
            throw new NotImplementedException();
        }

        public ListCustomerForList GetAllCustomersForList()
        {
            var customers = _repo.GetAllCustomers(); 
            ListCustomerForList result = new ListCustomerForList();
            result.Customers = new List<CustomerForListVm>();
            foreach (var customer in customers)
            {
                result.Customers.Add(new CustomerForListVm()
                { 
                    Id = customer.Id,
                    Name = customer.FirstName +" "+customer.LastName,
                    PhoneNumber = customer.PhoneNumber 
                });
            }
            return result;
        }

        public CustomerDetailsVm GetCustomerDetails(int customerId) /// Do poprawy 
        {
            var customer = _repo.GetCustomerById(customerId);
            CustomerDetailsVm result = new CustomerDetailsVm();
            result.Id= customer.Id;
            result.FullName = customer.FirstName+" "+customer.LastName; 
            result.PhoneNumber = customer.PhoneNumber;
            result.Address = customer.Address;
            result.City = customer.City;
            result.Region = customer.Region;    
            result.PostalCode = customer.PostalCode;
            result.Country = customer.Country;
            result.Orders = new List<Domain.Models.Order>();
            foreach (var order in customer.Orders)
            {
                result.Orders.Add(order);
            }
            return result;
        }
    }
}
