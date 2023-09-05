using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
            var model = _customerService.GetAllCustomersForList(5,1,"");

            return View(model); /// - WORKING, Sort by name, Pagination 
        }

        [HttpPost]
        public IActionResult Index(int pageSize, int? pageNo, string searchString)
        {
            if(!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (searchString is null)
            {
                searchString = string.Empty;
            }
            var model = _customerService.GetAllCustomersForList(pageSize,pageNo.Value,searchString);

            return View(model);
        }

        [Authorize]
        public IActionResult CustomerDetails(int id)
        {
            var detailInfo = _customerService.GetCustomerDetails(id);
            return View(detailInfo);
        }

        
        [HttpPost]
        [Authorize]
        public IActionResult AddCustomer(NewCustomerVm model)
        {
            var newCustomerId = _customerService.AddCustomer(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddCustomer()
        {
            return View(new NewCustomerVm());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _customerService.DeleteCustomer(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public IActionResult EditCustomer(int id)
        {
            var customer = _customerService.GetCustomerForEdit(id);
            return View(customer);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditCustomer(NewCustomerVm model)
        {
            _customerService.UpdateCustomer(model);
            return RedirectToAction("Index");
        }

    }
}
