using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
