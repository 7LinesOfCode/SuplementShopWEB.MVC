using SuplementShopWEB.MVC.Application.ViewModel.Order;
using SuplementShopWEB.MVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Application.Interfaces
{
    public interface IOrderService
    {
        public void CancelOrder(int orderId);
        public ListOrderForListVm GetActiveOrders();
        public ListOrderForListVm GetAllOrders();
        public bool CreateNewOrder(NewOrderVm newOrder);
    }
}
