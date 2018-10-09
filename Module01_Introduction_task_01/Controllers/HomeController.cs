using Microsoft.AspNetCore.Mvc;

namespace Northwind.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}