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
            var result = _orderService.GetActiveOrders(5,1,"");
            return View(result);
        }
        [HttpPost]
        public IActionResult Index(int pageSize, int? pageNo, string searchString)
        {
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (searchString is null)
            {
                searchString = string.Empty;
            }
            var model = _orderService.GetActiveOrders(pageSize, pageNo.Value, searchString);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var items = _itemService.GetAvailableItemsOnly();
            var itemList = new SelectList(items, "Id", "NamePrice");
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
            if(newOrderId != 0)
            {
                return View(newOrderId);
            }
            else
            {
                return View("NotCreated");
            }
            
        }

        [HttpGet]
        public IActionResult DownloadPdf(int id)
        {
            var pdfPath = $"Order{id}.pdf";

            var pdfBytes = System.IO.File.ReadAllBytes(pdfPath);
            var fileName = $"Order{id}.pdf";

            return File(pdfBytes, "application/pdf", fileName);
        }

        public IActionResult Completed(int id)
        {
            _orderService.Complete(id);

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id) 
        {
            var OrderDetails = _orderService.ById(id);

            return View(OrderDetails);
        }
    }
}
