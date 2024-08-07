using BusinessLayer.DTOS.AuthModels;
using BusinessLayer.Services.ServiceManager;
using Microsoft.AspNetCore.Mvc;

namespace SHATASK.Controllers
{
    public class AuthController : Controller
    {
        private readonly IServiceManager _serviceManager;
        public AuthController(IServiceManager servicemanager)
        {
            _serviceManager = servicemanager;
            
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _serviceManager.AuthService.Register(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Dashboard", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _serviceManager.AuthService.LoginAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Dashboard", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

    }
}
