using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Previewer;
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
        private readonly ICustomerRepository _customerRepo;
        public OrderService(IOrderRepository repo, IMapper mapper, IItemRepository itemRepo, ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
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

        public void CreatingPDF(OrderForListVm order)
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Size(PageSizes.A4);

                    page.Header()
                        .Text("Thank you for confidence. Your order is our priority!")
                        .SemiBold().FontSize(30).FontColor(Colors.Blue.Medium);
                    page.Content()
                        .Text($"Order id:  {order.Id}\n" +
                              $"Item id:  {order.ItemId} \n" +
                              $"Item Name:  {order.ItemName} x {order.CountOfItems}\n"+
                              $"Price:  {order.Price}$ \n" +
                              $"Full Name of Client:  {order.CustomerFullName}\n" +
                              $"Date of resive order:  {DateTime.UtcNow} \n"
                              );
                                

                    page.Footer()
                        .Text("Order document has been generate automaticly. " +
                        "Copyright © 2023 SuplementShop Inc. All rights reserved.");

                });
            })
            .GeneratePdf($"Order{order.Id}.pdf");
        }

        public int CreateNewOrder(NewOrderVm newOrderVm)
        {
            if(newOrderVm is null)
            {
                return 0;
            }
            var newOrder = _mapper.Map<Order>(newOrderVm);


            var itemId = newOrder.ItemId;
            var itemCount = newOrder.CountOfItems;

            var Item = _itemRepo.GetItemById(itemId);
            newOrder.Price = Item.Price*itemCount;

            var result = _repo.UpdateItemCount(itemId, itemCount);
            if (result == 0)
            {
                return result;
            }

            var id = _repo.AddNewOrder(newOrder);

            ///////// PDF DATA PREPARE

            var pdfData = new OrderForListVm();
            pdfData.Id = id;
            pdfData.ItemId = itemId;
            pdfData.Price = newOrder.Price;

            var Customer = _customerRepo.GetCustomerById(newOrderVm.CustomerId);
            pdfData.CustomerFullName =Customer.FirstName+" "+Customer.LastName;

            pdfData.CountOfItems = itemCount;

            var item = _itemRepo.GetItemById(itemId);
            pdfData.ItemName = item.Name;

            CreatingPDF(pdfData);
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
