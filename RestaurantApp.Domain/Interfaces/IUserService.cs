using RestaurantApp.Domain.DTOs;

namespace RestaurantApp.Domain.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(string id);
        Task<bool> UpdateUserAsync(UserDto userDto);
        Task<bool> DeleteUserAsync(string id);
    }
}