using Microsoft.AspNetCore.Mvc;
using TaskManagement.Web.Models.ViewModels;
using TaskManagement.Web.Services;

namespace TaskManagement.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthApiService _authService;

        public AuthController(AuthApiService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserId") != null)
                return RedirectToAction("Index", "Dashboard");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var response = await _authService.LoginAsync(model);
                if (response.Success && response.Data != null)
                {
                    HttpContext.Session.SetString("UserId", response.Data.UserId.ToString()!);
                    HttpContext.Session.SetString("FullName", response.Data.FullName);
                    HttpContext.Session.SetString("Role", response.Data.Role);
                    HttpContext.Session.SetString("AvatarUrl", response.Data.AvatarUrl ?? "");

                    return RedirectToAction("Index", "Dashboard");
                }
                ModelState.AddModelError("", response.Message);
            }
            catch
            {
                ModelState.AddModelError("", "Connection to API failed.");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var response = await _authService.RegisterAsync(model);
                if (response.Success)
                {
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("", response.Message);
            }
            catch
            {
                ModelState.AddModelError("", "Connection to API failed.");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
