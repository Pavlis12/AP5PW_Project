using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.Domain.Entities
{
    public class RestaurantTable
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public int Seats { get; set; }
        public string? Location { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
