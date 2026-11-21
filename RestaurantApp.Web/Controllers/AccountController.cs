using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Domain.Interfaces;

namespace RestaurantApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, bool rememberMe = false)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Vyplňte e-mail i heslo.");
                return View();
            }

            var success = await _authService.LoginAsync(email, password, rememberMe);
            if (success)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Neplatný e-mail nebo heslo.");
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string email, string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Vyplňte e-mail i heslo.");
                return View();
            }

            if (password != confirmPassword)
            {
                ModelState.AddModelError("", "Hesla se neshodují.");
                return View();
            }

            var (succeeded, errors) = await _authService.RegisterAsync(email, password);
            if (succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in errors)
            {
                ModelState.AddModelError("", error);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}