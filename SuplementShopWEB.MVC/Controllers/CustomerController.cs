using Microsoft.AspNetCore.Mvc;

namespace SuplementShopWEB.MVC.Controllers
{
    public class CustomerController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }
    }
}
