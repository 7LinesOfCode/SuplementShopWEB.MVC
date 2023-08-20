using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Application.ViewModel.Customer
{
    public class ListCustomerForList
    {
        public List<CustomerForListVm> Customers { get; set; }
        public int CurrentlyPage { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }
    }
}
