using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Domain.DTOs
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mail je povinný.")]
        [EmailAddress(ErrorMessage = "Neplatný formát e-mailu.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Uživatelské jméno je povinné.")]
        public string UserName { get; set; } = string.Empty;

        public string Role { get; set; } = "Customer";
    }
}