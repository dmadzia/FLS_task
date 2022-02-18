using FLS_task.Commerce.Cart.Models;
using FLS_task.Commerce.Cart.Services;
using FLS_task.Features.Cart.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FLS_task.Features.Cart.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public IActionResult Index()
        {
            var vm = new CartViewModel(cartService.GetCart());
            return View(vm);
        }

        public IActionResult AddToCart(string sku, int quantity = 1, string redirect = "/")
        {
            cartService.Add(new CartLineItem(sku, quantity));
            return Redirect(redirect);
        }

        public IActionResult RemoveFromCart(string sku, int quantity, string redirect = "/")
        {
            cartService.Remove(new CartLineItem(sku, quantity));
            return Redirect(redirect);
        }
    }
}
