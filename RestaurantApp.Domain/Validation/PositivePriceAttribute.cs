using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Domain.Validation
{
    public class PositivePriceAttribute : ValidationAttribute
    {
        public PositivePriceAttribute()
        {
            ErrorMessage = "Cena musí být větší než 0 Kč.";
        }

        public override bool IsValid(object? value)
        {
            if (value == null) return true;

            if (value is decimal price)
            {
                return price > 0;
            }

            return false;
        }
    }
}