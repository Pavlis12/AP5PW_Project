using RestaurantApp.Application.Interfaces;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Domain.Interfaces;

namespace RestaurantApp.Application.Services
{
    public class TableService : ITableService
    {
        private readonly IRepository<RestaurantTable> _repository;

        public TableService(IRepository<RestaurantTable> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RestaurantTable>> GetAllTablesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<RestaurantTable?> GetTableByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateTableAsync(RestaurantTable table)
        {
            await _repository.AddAsync(table);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateTableAsync(RestaurantTable table)
        {
            _repository.Update(table);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteTableAsync(int id)
        {
            var table = await _repository.GetByIdAsync(id);
            if (table != null)
            {
                _repository.Delete(table);
                await _repository.SaveChangesAsync();
            }
        }
    }
}