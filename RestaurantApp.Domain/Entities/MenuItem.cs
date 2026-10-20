using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.Domain.Entities
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int MenuCategoryId { get; set; }
        public MenuCategory MenuCategory { get; set; } = null!;
    }
}
