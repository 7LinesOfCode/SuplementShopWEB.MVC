using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuplementShopWEB.MVC.Application.Interfaces;
using SuplementShopWEB.MVC.Application.ViewModel.Order;

namespace SuplementShopWEB.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IItemService _itemService;
        private readonly ICustomerService _customerService;

        public OrderController(IOrderService service, IItemService itemService, ICustomerService customerService)
        {
            _itemService = itemService;
            _orderService = service;
            _customerService = customerService;
        }


        public IActionResult Index()
        {
            var result = _orderService.GetActiveOrders();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var items = _itemService.GetAvailableItemsOnly();
            var itemList = new SelectList(items, "Id", "Name");
            ViewData["Items"] = itemList;

            var customers = _customerService.GetAllCustomers();
            var cutsomerList = new SelectList(customers,"Id", "Name");
            ViewData["Customers"] = cutsomerList;

            var newOrder = new NewOrderVm();
           return View(newOrder);
        }

        [HttpPost]
        public IActionResult CreateOrder(NewOrderVm model)
        {
            Console.WriteLine($"CustomerId: {model.CustomerId}, CustomerFullName: {model.Customer}");
            var newOrderId = _orderService.CreateNewOrder(model);
            return View(newOrderId);
        }
    }
}
