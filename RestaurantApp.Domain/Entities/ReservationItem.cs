using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.Domain.Entities
{
    public class ReservationItem
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; } = null!;

        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; } = null!;

        public int Quantity { get; set; }


    }
}
