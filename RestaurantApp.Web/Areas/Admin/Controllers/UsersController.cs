using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantApp.Domain.DTOs;
using RestaurantApp.Domain.Interfaces;

namespace RestaurantApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            ViewBag.Roles = new SelectList(new[] { "Admin", "Customer" }, user.Role);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserDto userDto)
        {
            if (id != userDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var success = await _userService.UpdateUserAsync(userDto);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Nepodařilo se uložit změny uživatele.");
            }

            ViewBag.Roles = new SelectList(new[] { "Admin", "Customer" }, userDto.Role);
            return View(userDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}