using FLS_task.Commerce.Catalog.Models;

namespace FLS_task.Features.TestData
{
    internal static class TestVariants
    {
        internal static IEnumerable<Variant> Variants =>
            new[]
            {
                new Variant() { ProductId = 1, SKU = "vase_01", Price = 1.2 },
                new Variant() { ProductId = 2, SKU = "mug_XXL", Description = "Big mug", Price = 1 },
                new Variant() { ProductId = 3, SKU = "npp_01", Price = 0.45 },
            };
    }
}
