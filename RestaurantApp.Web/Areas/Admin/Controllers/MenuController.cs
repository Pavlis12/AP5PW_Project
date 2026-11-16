using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantApp.Application.Interfaces;
using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _menuService.GetAllMenuItemsAsync();
            return View(items);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _menuService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuItem menuItem)
        {
            ModelState.Remove("MenuCategory");

            if (ModelState.IsValid)
            {
                await _menuService.CreateMenuItemAsync(menuItem);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _menuService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", menuItem.MenuCategoryId);
            return View(menuItem);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _menuService.GetMenuItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            var categories = await _menuService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", item.MenuCategoryId);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MenuItem menuItem)
        {
            if (id != menuItem.Id)
            {
                return NotFound();
            }

            ModelState.Remove("MenuCategory");

            if (ModelState.IsValid)
            {
                await _menuService.UpdateMenuItemAsync(menuItem);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _menuService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", menuItem.MenuCategoryId);
            return View(menuItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _menuService.DeleteMenuItemAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}