using Microsoft.AspNetCore.Mvc;
using SuplementShopWEB.MVC.Application.Interfaces;

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
    }
}
