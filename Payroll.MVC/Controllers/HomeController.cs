using Microsoft.AspNetCore.Mvc;

namespace Payroll.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}