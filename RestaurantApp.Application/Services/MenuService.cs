using RestaurantApp.Application.Interfaces;
using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Application.Services
{
    public class MenuService : IMenuService
    {
        private readonly IRepository<MenuItem> _repository;

        // Využíváme Dependency Injection pro získání repozitáře
        public MenuService(IRepository<MenuItem> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MenuItem?> GetMenuItemByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateMenuItemAsync(MenuItem menuItem)
        {
            await _repository.AddAsync(menuItem);
            await _repository.SaveChangesAsync(); 
        }

        public async Task UpdateMenuItemAsync(MenuItem menuItem)
        {
            _repository.Update(menuItem); 
            await _repository.SaveChangesAsync(); 
        }

        public async Task DeleteMenuItemAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item != null)
            {
                _repository.Delete(item);
                await _repository.SaveChangesAsync(); 
            }
        }
    }
}