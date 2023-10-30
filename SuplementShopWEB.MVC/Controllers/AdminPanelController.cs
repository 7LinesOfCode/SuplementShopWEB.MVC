using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuplementShopWEB.MVC.Application.Interfaces;
using SuplementShopWEB.MVC.Application.ViewModel.AdminPanel.Users;

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

        
        public IActionResult DeleteAdmin(string id)
        {
            _userService.restrictPermissions(id);
            return RedirectToAction("GetAllAdmins");
        }


        [HttpPost]
        public IActionResult AddAdminPermission(UserVm newAdmin)
        {
            _userService.addPermissions(newAdmin.Email);
            return RedirectToAction("GetAllAdmins");
        }

        [HttpGet]
        public IActionResult AddAdminPermission()
        {
            var newAdmin = new UserVm();
            return View(newAdmin);
        }

        public IActionResult DeleteUser(string id)
        {
            _userService.deleteUserFromDB(id);
            return RedirectToAction("Index");
        }

    }
}
