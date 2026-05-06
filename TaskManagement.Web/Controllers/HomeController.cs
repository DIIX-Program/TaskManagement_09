using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }
    }
}
