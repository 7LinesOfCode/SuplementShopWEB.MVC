using SuplementShopWEB.MVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Infrastructure.Repositories
{
    public class OrderRepository
    {
        private readonly Context _context;
        public OrderRepository(Context context)
        {
            _context = context;
        }

        public IQueryable<Order> GetAllOrders()
        {
            return _context.Orders;
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id);
        }

        public Order GetOrderByCustomerId (int customerId)
        {
            return _context.Orders.FirstOrDefault(o => o.CustomerId == customerId);
        }

        public bool CheckOrderStatus(int id)
        {
            var order = GetOrderById(id);
            return order.isActive;
        }

    }
}
