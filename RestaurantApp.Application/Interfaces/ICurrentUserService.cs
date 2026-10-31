using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.Application.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
    }
}
