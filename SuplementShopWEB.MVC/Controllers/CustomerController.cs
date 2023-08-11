using Microsoft.AspNetCore.Mvc;
using SuplementShopWEB.MVC.Application.Interfaces;

namespace SuplementShopWEB.MVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService= customerService;
        }



        public IActionResult GetAllCustomers() 
        {
            var model = _customerService.GetAllCustomersForList();

            return View(model); /// IMPLEMENTED WITH AUTOMAPPER - WORKING
        }

        public IActionResult ViewCustomerDetails(int customerId)
        {
            var detailInfo = _customerService.GetCustomerDetails(customerId);
            return View(detailInfo);
        }

        [HttpGet]
        public IActionResult AddCustomer()
        {
            /// Not implemented
            return View();
        }


        [HttpPost]
        public IActionResult AddCustomer(int _id)
        {
            var newCustomerId = _customerService.AddCustomer();
            return View();
        }

        public IActionResult Index()
        {

            return View();/// Not implemented
        }
    }
}
