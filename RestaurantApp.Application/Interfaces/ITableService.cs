using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Application.Interfaces
{
    public interface ITableService
    {
        Task<IEnumerable<RestaurantTable>> GetAllTablesAsync();
        Task<RestaurantTable?> GetTableByIdAsync(int id);
        Task CreateTableAsync(RestaurantTable table);
        Task UpdateTableAsync(RestaurantTable table);
        Task DeleteTableAsync(int id);
    }
}