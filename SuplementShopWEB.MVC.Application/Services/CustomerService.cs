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

        public List<CustomerForListVm> GetAllCustomers()
        {
             var list = _repo.GetAllCustomers(); 
            List <CustomerForListVm> ReturnList = new List<CustomerForListVm>();
            foreach (var item in list)
            {
                var customer = new CustomerForListVm();
                customer.Id = item.Id;
                customer.Name = item.FirstName+" "+item.LastName;
                ReturnList.Add(customer);
            }
            return ReturnList;
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
                PageSize = pageSize,
                CurrentlyPage = pageNo,
                SearchString= searchString,
                Customers = customerToShow,
                Count = customers.Count
            };
            return customerList;

        }
        
        public CustomerDetailsVm GetCustomerDetails(int customerId) /// Not tested yet ??? 
        {
            var customer = _repo.GetCustomerById(customerId);
            var customerVm = _mapper.Map<CustomerDetailsVm>(customer);
/*            if(customer.Orders != null)
            {
                foreach (var order in customer.Orders)
                {
                    customerVm.Orders.Add(order);
                }
            }*/

            return customerVm;
        }

        public NewCustomerVm GetCustomerForEdit(int id)
        {
            var customer = _repo.GetCustomerById(id);
            var customerVm = _mapper.Map<NewCustomerVm>(customer);
            return customerVm;
        }

        public void UpdateCustomer(NewCustomerVm model)
        {
            var customer = _mapper.Map<Customer>(model);
            _repo.UpdateCustomer(customer);
        }
    }
}
