using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuplementShopWEB.MVC.Application.Interfaces;

namespace SuplementShopWEB.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
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

        [HttpGet]
        public IActionResult GetAllAdmins()
        {
            var admins = _userService.GetAdmins();
            return View(admins);
        }
    }
}
