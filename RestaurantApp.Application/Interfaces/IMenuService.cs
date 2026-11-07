using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Application.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync();
        Task<MenuItem?> GetMenuItemByIdAsync(int id);
        Task CreateMenuItemAsync(MenuItem menuItem);
        Task UpdateMenuItemAsync(MenuItem menuItem);
        Task DeleteMenuItemAsync(int id);
    }
}