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

        [HttpGet]
        public IActionResult Index()
        { 
            var model = _itemService.GetAllItems(5, 1, "",false);
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(int pageSize, int? pageNo, string searchString,bool isAvailable)
        {
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (searchString is null)
            {
                searchString = string.Empty;
            }

            var model = _itemService.GetAllItems(pageSize, pageNo.Value, searchString, isAvailable);
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddItem()
        {
            var types = _itemService.GetListOfTypes();
            var typeList = new SelectList(types, "Id", "Name");
            ViewData["Types"] = typeList;
            var newItemVm = new NewItemVm();
            return View(newItemVm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddItem(NewItemVm model)
        {
            var newCustomerId = _itemService.AddItem(model);
            return RedirectToAction("Index");
        }


        [Authorize]
        public IActionResult Delete(int id)
        {
            _itemService.DeleteItem(id);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var types = _itemService.GetListOfTypes();
            var typeList = new SelectList(types, "Id", "Name");
            ViewData["Types"] = typeList;
            var item = _itemService.GetItemByIdEdit(id);
            return View(item);
        }
        // In progress!

        [Authorize]
        [HttpPost]
        public IActionResult Edit(NewItemVm item)
        {
            _itemService.UpdateItem(item);
            return RedirectToAction("Index");
        }


        
        public IActionResult Details(int id)
        {
            var item = _itemService.GetItemByIdEdit(id);
            ViewData["TypeName"] = _itemService.GetTypeNameByTypeId(item.TypeId);
            return View(item);
        }

    }
}
