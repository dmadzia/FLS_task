using FLS_task.Commerce.Discounts.Models;

namespace FLS_task.Commerce.Cart.Models
{
    public class CartLineItem
    {
        public CartLineItem(string sku, int quantity = 1)
        {
            SKU = sku;
            Quantity = quantity;
        }

        public string SKU { get; set; }
        public int Quantity { get; set; }
        public double PricePerItem { get; set; }
        public double EffectiveLinePrice { get; set; }
        public string? AppliedDiscountDescription { get; set; }
    }
}
