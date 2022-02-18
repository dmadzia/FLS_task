using FLS_task.Commerce.Cart.Models;
using FLS_task.Commerce.Discounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLS_task.Commerce.Discounts.Services
{
    public class DiscountsProcessor : IDiscountsProcessor
    {
        private readonly IDiscountsProvider _discountsProvider;

        public DiscountsProcessor(IDiscountsProvider discountsProvider)
        {
            _discountsProvider = discountsProvider;
        }

        public void ApplyDiscounts(CartData cartData)
        {
            foreach(var cartItem in cartData.Items)
            {
                var discounts = _discountsProvider.GetDiscounts(cartItem.SKU);
                double bestPrice = cartItem.EffectiveLinePrice;
                Discount? appliedDiscount = null;
                foreach(var discount in discounts)
                {
                    if (discount.ApplyDiscount(cartItem, out double price))
                    {
                        if (bestPrice > price)
                        {
                            appliedDiscount = discount;
                            bestPrice = price;
                        }
                    }
                }
                if (appliedDiscount != null)
                {
                    cartItem.AppliedDiscountDescription = appliedDiscount.Description;
                    cartItem.EffectiveLinePrice = bestPrice;
                }
            }
        }
    }
}
