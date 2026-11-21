namespace RestaurantApp.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string email, string password, bool rememberMe);
        Task<(bool Succeeded, IEnumerable<string> Errors)> RegisterAsync(string email, string password);
        Task LogoutAsync();
    }
}