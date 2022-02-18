using FLS_task.Commerce.Cart.Models;
using FLS_task.Commerce.Cart.Repository;
using FLS_task.Commerce.Cart.Services;
using FLS_task.Commerce.Catalog.Models;
using FLS_task.Commerce.Discounts.Models;
using FLS_task.Commerce.Discounts.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace FLS_task.Commerce.Tests
{
    [TestClass]
    public class SessionCartTests
    {
        private readonly Variant[] variants = new[]
        {
            new Variant() { SKU = "v1", Price = 1.5 },
            new Variant() { SKU = "v2", Price = 1 },
            new Variant() { SKU = "v3", Price = 3 },
        };

        private readonly Discount[] discounts = new[]
        {
            new MultiItemsDiscount("v2", 2, 1.5),
            new MultiItemsDiscount("v3", 3, 7),
        };

        [TestMethod]
        public void AddToCartTest()
        {
            var cartValidatorMock = new Mock<ICartItemValidationService>();
            Variant variant = variants[0];
            cartValidatorMock.Setup(v => v.Validate(It.IsAny<CartLineItem>(), out variant)).Returns(true);

            CartService sessionCartService = new(GetCartRepositoryMock(), cartValidatorMock.Object, null);
            sessionCartService.Add(new CartLineItem(variant.SKU, 1));
            var cart = sessionCartService.GetCart();

            Assert.IsNotNull(cart);
            var cartItem = cart.Items.Where(i => i.SKU == variant.SKU).FirstOrDefault();
            Assert.IsNotNull(cartItem);
            Assert.AreEqual(1.5, cartItem.EffectiveLinePrice);
            Assert.AreEqual(1.5, sessionCartService.GetTotal());
        }

        [TestMethod]
        public void AddToCartWithDiscountTest()
        {
            var cartValidatorMock = new Mock<ICartItemValidationService>();
            Variant variant = variants[1];
            cartValidatorMock.Setup(v => v.Validate(It.IsAny<CartLineItem>(), out variant)).Returns(true);

            DiscountsProcessor discountsProcessor = new(GetDiscountsProvider(variant.SKU));

            CartService sessionCartService = new(GetCartRepositoryMock(), cartValidatorMock.Object, discountsProcessor);
            sessionCartService.Add(new CartLineItem(variant.SKU, 5));
            var cart = sessionCartService.GetCart();

            Assert.IsNotNull(cart);
            var lineItem = cart.Items.Where(i => i.SKU == variant.SKU).FirstOrDefault();
            Assert.IsNotNull(lineItem);
            Assert.AreEqual(5, lineItem.Quantity);
            Assert.AreEqual(4, lineItem.EffectiveLinePrice);
            Assert.AreEqual(4, sessionCartService.GetTotal());
        }

        [TestMethod]
        public void AddToCartTwoItemsTest()
        {
            var variant1 = variants[0];
            var variant2 = variants[1];

            var cartValidatorMock = new Mock<ICartItemValidationService>();
            cartValidatorMock.Setup(v => v.Validate(It.Is<CartLineItem>(i => i.SKU == variants[0].SKU), out variants[0])).Returns(true);
            cartValidatorMock.Setup(v => v.Validate(It.Is<CartLineItem>(i => i.SKU == variants[1].SKU), out variants[1])).Returns(true);

            CartService sessionCartService = new(GetCartRepositoryMock(), cartValidatorMock.Object, null);

            sessionCartService.Add(new CartLineItem(variant1.SKU, 1));
            sessionCartService.Add(new CartLineItem(variant2.SKU, 2));
            
            var cart = sessionCartService.GetCart();

            Assert.IsNotNull(cart);
            Assert.IsNotNull(cart.Items.Where(i => i.SKU == variant1.SKU).FirstOrDefault());
            Assert.IsNotNull(cart.Items.Where(i => i.SKU == variant2.SKU).FirstOrDefault());
            Assert.AreEqual(2, cart.Items.Count());
            Assert.AreEqual(3, cart.Items.Sum(i => i.Quantity));
            Assert.AreEqual(3.5, sessionCartService.GetTotal());
        }

        [TestMethod]
        public void RemoveFromCartTest()
        {
            var cartValidatorMock = new Mock<ICartItemValidationService>();
            var variant = variants[1];
            cartValidatorMock.Setup(v => v.Validate(It.IsAny<CartLineItem>(), out variant)).Returns(true);

            CartService sessionCartService = new(GetCartRepositoryMock(), cartValidatorMock.Object, null);

            sessionCartService.Add(new CartLineItem(variant.SKU, 1));

            Assert.IsNotNull(sessionCartService.GetCart().Items.Where(i => i.SKU == variant.SKU).FirstOrDefault());

            sessionCartService.Remove(new CartLineItem(variant.SKU, 1));

            Assert.IsNull(sessionCartService.GetCart().Items.Where(i => i.SKU == variant.SKU).FirstOrDefault());
        }

        private ICartRepository GetCartRepositoryMock()
        {
            var cartRepositoryMock = new Mock<ICartRepository>();
            var cart = new CartData();
            cartRepositoryMock.Setup(repo => repo.GetCart()).Returns(cart);
            cartRepositoryMock.Setup(repo => repo.SaveCart(It.IsAny<CartData>())).Callback<CartData>(cartResult => cart = cartResult);
            return cartRepositoryMock.Object;
        }

        private IDiscountsProvider GetDiscountsProvider(string sku)
        {
            var discountsProviderMock = new Mock<IDiscountsProvider>();
            discountsProviderMock.Setup(dp => dp.GetDiscounts(It.IsAny<string>())).Returns(discounts.Where(d => d.SKU == sku));
            return discountsProviderMock.Object;
        }
    }
}