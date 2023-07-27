using Microsoft.AspNetCore.Mvc;
using SuplementShopWEB.MVC.Application.Interfaces;

namespace SuplementShopWEB.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService service)
        {
            _orderService = service;
        }


        public IActionResult AllOrderes()
        {
            var result = _orderService.GetAllOrders();
            return View(result);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
