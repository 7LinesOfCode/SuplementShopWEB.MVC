using SuplementShopWEB.MVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Domain.Interface
{
    public interface IOrderRepository
    {
        public IQueryable<Order> GetAllOrders();
        public int AddNewOrder(Order order);
        public Order GetOrderById(int id);
        public Order GetOrderByCustomerId(int customerId);
        public bool CheckOrderStatus(int id);
        public int UpdateItemCount(int itemId, int minusCount);
    }
}
