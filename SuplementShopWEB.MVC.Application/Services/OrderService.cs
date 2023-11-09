using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuplementShopWEB.MVC.Application.Interfaces;
using SuplementShopWEB.MVC.Application.ViewModel.Customer;
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
        private readonly IItemRepository _itemRepo;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository repo, IMapper mapper, IItemRepository itemRepo)
        {
            _mapper = mapper;
            _repo = repo;
            _itemRepo = itemRepo;
        }

        public void CancelOrder(int orderId)
        {
            var OrderToCancel =  _repo.GetOrderById(orderId);
            if(!OrderToCancel.isDone)
            {
                OrderToCancel.isDone = true;
            }
        }

        public int CreateNewOrder(NewOrderVm newOrderVm)
        {
            if(newOrderVm is null)
            {
                return 0;
            }
            var newOrder = _mapper.Map<Order>(newOrderVm);
            var id = _repo.AddNewOrder(newOrder);

            if(id != null)
            {
                var itemId = newOrder.ItemId;
                var itemCount = newOrder.CountOfItems;

                _repo.UpdateItemCount(itemId, itemCount);
            }

            return id;
        }

        public ListOrderForListVm GetActiveOrders(int pageSize, int pageNo, string searchString)
        {
            var activeOrders =  _repo.GetAllOrders()
                .Where(o=>o.isDone == false)
                .ProjectTo<OrderForListVm>(_mapper.ConfigurationProvider)
                .Where(c=>(c.CustomerFullName).Contains(searchString))
                .ToList();

            var ordersToShow = activeOrders.Skip(pageSize *(pageNo -1)).Take(pageSize).ToList();

            var resultVm = new ListOrderForListVm()
            {
                PageSize = pageSize,
                CurrentlyPage = pageNo,
                SearchString = searchString,
                Orders = ordersToShow,
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
