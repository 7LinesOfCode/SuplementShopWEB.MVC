﻿using SuplementShopWEB.MVC.Application.ViewModel.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Application.Interfaces
{
    public interface ICustomerService
    {
        public ListCustomerForList GetAllCustomersForList(int pageSize, int pageNo, string searchString);
        public int AddCustomer(NewCustomerVm newCustomerModel);
        public int AddCustomer();
        public CustomerDetailsVm GetCustomerDetails(int customerId);

    }
}
