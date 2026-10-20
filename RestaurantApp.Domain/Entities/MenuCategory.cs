using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.Domain.Entities
{
    public class MenuCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}
