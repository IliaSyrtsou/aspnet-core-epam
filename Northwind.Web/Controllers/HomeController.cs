using Microsoft.AspNetCore.Mvc;

namespace Northwind.Web.Controllers
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