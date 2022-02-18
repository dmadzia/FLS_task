using FLS_task.Commerce.Cart.Models;

namespace FLS_task.Commerce.Cart.Services
{
    public interface ICartService
    {
        void Add(CartLineItem item);
        void Remove(CartLineItem item);
        double GetTotal();
        CartData GetCart();
    }

}