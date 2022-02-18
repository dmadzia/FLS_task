using FLS_task.Commerce.Cart.Models;
using System.Text.Json.Serialization;

namespace FLS_task.Commerce.Discounts.Models
{
    public class MultiItemsDiscount : Discount
    {
        public MultiItemsDiscount(string sku, int numberOfItems, double discountPrice) : base(sku)
        {
            NumberOfItems = numberOfItems;
            DiscountPrice = discountPrice;
        }

        public int NumberOfItems { get; set; }
        public double DiscountPrice { get; set; }

        public override string Description => $"Discount for multiple items in the order. {NumberOfItems} items for {DiscountPrice:0.00} €.";

        public override bool ApplyDiscount(CartLineItem cartLineItem, out double price)
        {
            price = Math.Floor(cartLineItem.Quantity / (double)NumberOfItems) * DiscountPrice 
                + (cartLineItem.Quantity % NumberOfItems) * cartLineItem.PricePerItem;
            
            return cartLineItem.Quantity >= NumberOfItems;
        }
    }
}
