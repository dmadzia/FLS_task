using FLS_task.Commerce.Discounts.Models;

namespace FLS_task.Commerce.TestData
{
    internal static class TestDiscounts
    {
        internal static IEnumerable<Discount> Discounts =>
            new[] { 
                new MultiItemsDiscount("mug_XXL", 2, 1.5),
                new MultiItemsDiscount("npp_01", 3, 0.9),
            };
    }
}
