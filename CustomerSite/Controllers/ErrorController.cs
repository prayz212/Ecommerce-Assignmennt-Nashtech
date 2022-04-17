using Microsoft.AspNetCore.Mvc;

namespace CustomerSite.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
