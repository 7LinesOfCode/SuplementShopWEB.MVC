using AutoMapper;
using AutoMapper.QueryableExtensions;
using SuplementShopWEB.MVC.Application.Interfaces;
using SuplementShopWEB.MVC.Application.ViewModel.Order;
using SuplementShopWEB.MVC.Domain.Interface;
using SuplementShopWEB.MVC.Domain.Models;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace SuplementShopWEB.MVC.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository repo, IMapper mapper)
        {
            _mapper= mapper;
            _repo = repo;
        }

        public void CancelOrder(int orderId)
        {
            var OrderToCancel =  _repo.GetOrderById(orderId);
            if(!OrderToCancel.isDone)
            {
                OrderToCancel.isDone = true;
            }
        }

        public bool CreateNewOrder(NewOrderVm newOrderVm)
        {
            if(newOrderVm is null)
            {
                return false;
            }
            var newOrder = new SuplementShopWEB.MVC.Domain.Models.Order();
            newOrder.Id = newOrderVm.Id;
            newOrder.ItemId = newOrderVm.ItemId;
            //// to implament 
            ///
            throw new NotImplementedException();

        }

        public ListOrderForListVm GetActiveOrders()
        {
            var activeOrders =  _repo.GetAllOrders()
                .Where(o=>o.isDone == false)
                .ProjectTo<OrderForListVm>(_mapper.ConfigurationProvider)
                .ToList();

            var resultVm = new ListOrderForListVm()
            {
                Orders = activeOrders,
                Count = activeOrders.Count
            };
            return resultVm;

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
