using FLS_task.Commerce.Cart.Models;
using System.Text.Json.Serialization;

namespace FLS_task.Commerce.Discounts.Models
{
    public abstract class Discount
    {
        public Discount(string sku)
        {
            SKU = sku;
        }

        public string SKU { get; set; }
        public virtual string Description => "";

        public abstract bool ApplyDiscount(CartLineItem cartLineItem, out double price);
    }
}
