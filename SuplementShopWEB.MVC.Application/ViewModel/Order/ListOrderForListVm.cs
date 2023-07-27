using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Application.ViewModel.Order
{
    public class ListOrderForListVm
    {
        public List<OrderForListVm> Orders { get; set; }
        public int Count { get; set; }
    }
}
