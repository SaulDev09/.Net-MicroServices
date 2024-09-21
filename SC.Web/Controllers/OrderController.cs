using Microsoft.AspNetCore.Mvc;

namespace SC.Web.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult OrderIndex()
        {
            return View();
        }
    }
}
