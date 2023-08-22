using AutoMapper;
using AutoMapper.QueryableExtensions;
using SuplementShopWEB.MVC.Application.Interfaces;
using SuplementShopWEB.MVC.Application.ViewModel.Customer;
using SuplementShopWEB.MVC.Domain.Interface;
using SuplementShopWEB.MVC.Domain.Models;
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
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository repo, IMapper mapper)
        {
            _repo= repo;
            _mapper= mapper;    
        }


        public int AddCustomer(NewCustomerVm customer)
        {
            var cust = _mapper.Map<Customer>(customer);
            var id = _repo.AddNewCustomer(cust);
            return id;
        }

        public void DeleteCustomer(int customerId)
        {
             _repo.DeleteCustomer(customerId);
        }

        public ListCustomerForList GetAllCustomersForList(int pageSize, int pageNo, string searchString)
        {
            var customers = _repo
                .GetAllCustomers()
                .Where(c =>(c.FirstName+c.LastName).Contains(searchString))
                .ProjectTo<CustomerForListVm>(_mapper.ConfigurationProvider)
                .ToList();

            var customerToShow = customers.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();

            var customerList = new ListCustomerForList()
            {
                Customers = customerToShow,
                Count = customers.Count,
                PageSize = pageSize,
                CurrentlyPage = pageNo,
                SearchString= searchString
            };
            return customerList;

        }
        
        public CustomerDetailsVm GetCustomerDetails(int customerId) /// Not tested yet ??? 
        {
            var customer = _repo.GetCustomerById(customerId);
            var customerVm = _mapper.Map<CustomerDetailsVm>(customer);
            foreach (var order in customer.Orders)
            {
                customerVm.Orders.Add(order);
            }
            return customerVm;
        }
    }
}
