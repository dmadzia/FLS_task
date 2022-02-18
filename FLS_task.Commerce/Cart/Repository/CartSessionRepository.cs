using FLS_task.Commerce.Cart.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FLS_task.Commerce.Cart.Repository
{
    public class CartSessionRepository : ICartRepository
    {
        private const string CART_SESSION_KEY = "CART";

        private readonly IHttpContextAccessor httpContextAccessor;

        public CartSessionRepository(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public void SaveCart(CartData cartData)
        {
            httpContextAccessor.HttpContext.Session.Set(CART_SESSION_KEY, JsonSerializer.SerializeToUtf8Bytes(cartData));
        }
        public CartData GetCart()
        {
            if (httpContextAccessor.HttpContext.Session.TryGetValue(CART_SESSION_KEY, out var sessionValue))
                return (CartData?)JsonSerializer.Deserialize(sessionValue, typeof(CartData)) ?? new CartData();
            return new CartData();
        }
    }
}
