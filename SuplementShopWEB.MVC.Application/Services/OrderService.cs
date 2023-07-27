using SuplementShopWEB.MVC.Application.Interfaces;
using SuplementShopWEB.MVC.Application.ViewModel.Order;
using SuplementShopWEB.MVC.Domain.Interface;
using SuplementShopWEB.MVC.Domain.Models;

namespace SuplementShopWEB.MVC.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }

        public void CancelOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public void CreateNewOrder()
        {
            throw new NotImplementedException();
        }

        public ListOrderForListVm GetActiveOrders()
        {
            throw new NotImplementedException();
        }

        public ListOrderForListVm GetAllOrders()
        {
            var orders = _repo.GetAllOrders();
            var resultVm = new ListOrderForListVm();
            resultVm.Orders = new List<OrderForListVm>();
            foreach (var order in orders)
            {
                var singleOrderVm = new OrderForListVm();
                singleOrderVm.Id = order.Id;
                singleOrderVm.CustomerFullName = order.Customer.FirstName + " " + order.Customer.LastName;
                singleOrderVm.CustomerId = order.Customer.Id;
                singleOrderVm.PhoneNumber = order.Customer.PhoneNumber;
                singleOrderVm.ItemId= order.ItemId;
                singleOrderVm.ItemName = order.Item.Name;
                singleOrderVm.Price = order.Price;

                resultVm.Orders.Add(singleOrderVm);
            }
            return resultVm;
        }
    }
}
