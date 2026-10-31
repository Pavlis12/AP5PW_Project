using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> EnsureCustomerRole()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;

                if (userName != "admin@restaurace.cz")
                {
                    var user = await _userManager.FindByNameAsync(userName);

                    if (user != null)
                    {
                        if (!await _userManager.IsInRoleAsync(user, "Customer"))
                        {
                            await _userManager.AddToRoleAsync(user, "Customer");
                        }
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}