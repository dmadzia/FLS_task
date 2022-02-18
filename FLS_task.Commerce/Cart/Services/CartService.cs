using FLS_task.Commerce.Cart.Models;
using FLS_task.Commerce.Cart.Repository;
using FLS_task.Commerce.Catalog.Models;
using FLS_task.Commerce.Catalog.Services;
using FLS_task.Commerce.Discounts.Services;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Linq;

namespace FLS_task.Commerce.Cart.Services
{
    public class CartService : ICartService
    {
        private readonly ICartItemValidationService cartItemValidationService;
        private readonly IDiscountsProcessor? discountsProcessor;
        private readonly ICartRepository cartRepository;

        public CartService(ICartRepository cartRepository, ICartItemValidationService cartItemValidationService, IDiscountsProcessor? discountsProcessor)
        {
            this.cartItemValidationService = cartItemValidationService;
            this.discountsProcessor = discountsProcessor;
            this.cartRepository = cartRepository;
        }

        public void Add(CartLineItem item)
        {
            if (!cartItemValidationService.Validate(item, out Variant variant))
                return;

            var cart = GetCart();
            
            var cartItem = cart.Items.Where(i => i.SKU == item.SKU)?.FirstOrDefault();
            if (cartItem == default(CartLineItem))
            {
                cartItem = item;
                cartItem.PricePerItem = variant.Price;
                cart.Items = cart.Items.Concat(new[] { cartItem });
            }
            else
            {
                cartItem.Quantity += item.Quantity;
            }
            cartItem.EffectiveLinePrice = cartItem.PricePerItem * cartItem.Quantity;

            ProcessCartChanges(cart);            
        }

        public void Remove(CartLineItem item)
        {
            var cart = GetCart();

            var cartItem = cart.Items.Where(i => i.SKU == item.SKU)?.FirstOrDefault();
            if (cartItem != default(CartLineItem))
            {
                cartItem.Quantity -= item.Quantity;
                cartItem.EffectiveLinePrice = cartItem.PricePerItem * cartItem.Quantity;
                if (cartItem.Quantity <= 0)
                {
                    cart.Items = cart.Items.Where(i => i.SKU != item.SKU);
                }
            }            

            ProcessCartChanges(cart);
        }

        public double GetTotal()
        {
            return cartRepository.GetCart().TotalValue;
        }        

        public CartData GetCart()
        {
            return cartRepository.GetCart();
        }

        private void ProcessCartChanges(CartData cart)
        {
            ApplyDiscounts(cart);
            CalculateTotal(cart);
            cartRepository.SaveCart(cart);
        }

        private void ApplyDiscounts(CartData cart)
        {
            discountsProcessor?.ApplyDiscounts(cart);
        }        

        private static void CalculateTotal(CartData cartData)
        {
            cartData.TotalValue = cartData.Items.Sum(i => i.EffectiveLinePrice);
        }        
    }
}
