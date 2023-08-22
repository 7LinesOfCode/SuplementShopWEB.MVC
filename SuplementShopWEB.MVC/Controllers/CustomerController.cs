using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SuplementShopWEB.MVC.Application.Interfaces;
using SuplementShopWEB.MVC.Application.ViewModel.Customer;
using SuplementShopWEB.MVC.Domain.Models;

namespace SuplementShopWEB.MVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        

        public CustomerController(ICustomerService customerService)
        {
            
            _customerService= customerService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var model = _customerService.GetAllCustomersForList(3,1, "");

            return View(model); /// - WORKING, Sort by name, Pagination 
        }

        [HttpPost]
        public IActionResult Index(int pageSize, int? pageNo, string searchString)
        {
            if(!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (searchString == null)
            {
                searchString = string.Empty;
            }
            var model = _customerService.GetAllCustomersForList(pageSize,pageNo.Value,searchString);

            return View(model);
        }

        public IActionResult ViewCustomerDetails(int customerId)
        {
            var detailInfo = _customerService.GetCustomerDetails(customerId);
            return View(detailInfo);
        }

        [HttpPost]
        public IActionResult AddCustomer(NewCustomerVm model)
        {
            var newCustomerId = _customerService.AddCustomer(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddCustomer()
        {
            return View(new NewCustomerVm());
        }

    }
}
