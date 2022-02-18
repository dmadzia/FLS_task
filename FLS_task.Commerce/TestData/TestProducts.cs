using FLS_task.Commerce.Catalog.Models;

namespace FLS_task.Features.TestData
{
    internal static class TestProducts
    {
        internal static IEnumerable<Product> Products =>
            new[]
            {
                new Product { Id = 1, Name = "Vase" },
                new Product { Id = 2, Name = "Mug" },
                new Product { Id = 3, Name = "Napkins pack" }
            };
    }
}
