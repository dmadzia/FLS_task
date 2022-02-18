using FLS_task.Commerce.Cart.Models;

namespace FLS_task.Commerce.Discounts.Services
{
    public interface IDiscountsProcessor
    {
        void ApplyDiscounts(CartData cartData);
    }
}
