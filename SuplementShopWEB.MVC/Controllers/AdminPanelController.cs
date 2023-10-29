using Microsoft.AspNetCore.Mvc;
using SuplementShopWEB.MVC.Application.Interfaces;

namespace SuplementShopWEB.MVC.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly IUserService _userService;
        public AdminPanelController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _userService.GetUserVm();
            return View(users);
        }
    }
}
