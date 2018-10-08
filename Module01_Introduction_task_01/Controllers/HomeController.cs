using Microsoft.AspNetCore.Mvc;

namespace Module01_Introduction_task_01.Controllers
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