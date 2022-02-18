using FLS_task.Commerce.Cart.Models;
using FLS_task.Commerce.Catalog.Models;

namespace FLS_task.Commerce.Cart.Services
{
    public interface ICartItemValidationService
    {
        bool Validate(CartLineItem item, out Variant v);
    }

}