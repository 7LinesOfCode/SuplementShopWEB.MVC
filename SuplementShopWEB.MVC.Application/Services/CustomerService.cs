using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository repo, IMapper mapper)
        {
            _repo= repo;
            _mapper= mapper;    
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
            var customers = _repo
                .GetAllCustomers()
                .ProjectTo<CustomerForListVm>(_mapper.ConfigurationProvider)
                .ToList();
            var customerList = new ListCustomerForList()
            {
                Customers = customers,
                Count = customers.Count
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
