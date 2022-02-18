using FLS_task.Commerce.Cart.Models;
using FLS_task.Commerce.Catalog.Models;
using FLS_task.Commerce.Catalog.Services;

namespace FLS_task.Commerce.Cart.Services
{
    public class CartItemValidationService : ICartItemValidationService
    {
        private readonly ICatalogProvider catalogProviderService;

        public CartItemValidationService(ICatalogProvider catalogProviderService)
        {
            this.catalogProviderService = catalogProviderService;
        }

        public bool Validate(CartLineItem item, out Variant v)
        {
            v = catalogProviderService.GetVariant(item.SKU);
            return v != default(Variant);
        }
    }

}