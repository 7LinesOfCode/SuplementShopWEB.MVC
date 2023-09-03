using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuplementShopWEB.MVC.Application.Interfaces;
using SuplementShopWEB.MVC.Application.Services;
using SuplementShopWEB.MVC.Application.ViewModel.Customer;
using SuplementShopWEB.MVC.Application.ViewModel.Item;
using SuplementShopWEB.MVC.Domain.Models;

namespace SuplementShopWEB.MVC.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        public IActionResult Index()
        { 
            var model = _itemService.GetAllItems();
            return View(model);
        }

        /*        [HttpGet]
                public IActionResult AddItem()
                {
                    var types = _itemService.GetListOfTypes();
                    ViewData["Types"] = new SelectList(types, "Id", "Name"); // "Id" i "Nazwa" to odpowiednie właściwości w Twoim typie
                    return View(new NewItemVm());
                }*/

        [HttpGet]
        public IActionResult AddItem()
        {
            var types = _itemService.GetListOfTypes();
            var typeList = new SelectList(types, "Id", "Name");
            ViewData["Types"] = typeList;
            var newItemVm = new NewItemVm();
            return View(newItemVm);
        }

        [HttpPost]
        public IActionResult AddItem(NewItemVm model)
        {
            var newCustomerId = _itemService.AddItem(model);
            return RedirectToAction("Index");
        }

       
        public IActionResult Delete(int id)
        {
            _itemService.DeleteItem(id);
            return RedirectToAction("Index");
        }
    }
}
