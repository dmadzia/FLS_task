using FLS_task.Commerce.Cart.Models;

namespace FLS_task.Features.Cart.ViewModels
{
    public class CartViewModel
    {
        public CartViewModel(CartData cartData)
        {
            CartData = cartData;
        }

        public CartData CartData { get; set; }        
    }
}
