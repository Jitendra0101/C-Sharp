using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index(int? id, int a = 5, int b = 10)
        {
            ViewBag.id = id;
            ViewBag.a = a;
            ViewBag.b = b;

            return View();
        }
    }
}
