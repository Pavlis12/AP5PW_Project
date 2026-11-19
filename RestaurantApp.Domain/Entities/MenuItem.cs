using System.ComponentModel.DataAnnotations;
using RestaurantApp.Domain.Validation;

namespace RestaurantApp.Domain.Entities
{
    public class MenuItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Název jídla je povinný.")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [PositivePrice]
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int MenuCategoryId { get; set; }
        public MenuCategory MenuCategory { get; set; } = null!;
    }
}