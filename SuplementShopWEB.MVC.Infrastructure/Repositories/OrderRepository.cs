using SuplementShopWEB.MVC.Domain.Interface;
using SuplementShopWEB.MVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SuplementShopDbContext _context;
        public OrderRepository(SuplementShopDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> GetAllOrders()
        {
            return _context.Orders;
        }

        public int UpdateItemCount(int itemId, int minusCount)
        {
            var item = _context.Items.FirstOrDefault(c => c.Id == itemId);
            if(item.Count >= minusCount)
            {
                item.Count -= minusCount;
                _context.Items.Attach(item);
                _context.Entry(item).Property("Count").IsModified = true;
            }
            else
            {
                return 0;
            }

            if(item.Count <= 0)
            {
                item.IsAvailable = false;
                _context.Entry(item).Property("IsAvailable").IsModified= true;
            }
            _context.SaveChanges();

            return 1;
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
            return order.isDone;
        }   

        public int AddNewOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges(); 
            return order.Id;
        }
    }
}
