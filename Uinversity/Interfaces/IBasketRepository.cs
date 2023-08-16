﻿using Uinversity.Entities;

namespace Uinversity.Interfaces
{
    public interface IBasketRepository
    {
        Task<Cart?> GetBasket(string userName);
        Task<Cart?> UpdateBasket(Cart basket);
        Task DeleteBasket(string userName);
    }
}