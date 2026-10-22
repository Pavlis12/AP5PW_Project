using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string? CustomerPhone { get; set; }
        public DateTime ReservationTime { get; set; }
        public int NumberOfPeople { get; set; }
        public string? Note { get; set; }
        public ICollection<ReservationItem> PreorderedMeals { get; set; } = new List<ReservationItem>();
        public int RestaurantTableId { get; set; }
        public RestaurantTable RestaurantTable { get; set; } = null!;

    }
}
