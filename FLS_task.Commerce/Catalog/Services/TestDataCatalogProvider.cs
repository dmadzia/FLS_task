using FLS_task.Commerce.Catalog.Models;
using FLS_task.Features.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLS_task.Commerce.Catalog.Services
{
    public class TestDataCatalogProvider : ICatalogProvider
    {
        public TestDataCatalogProvider()
        {

        }

        public IEnumerable<Product> GetProducts()
        {
            return TestProducts.Products;
        }

        public Variant GetVariant(string sku)
        {
            return TestVariants.Variants.Where(v => v.SKU == sku).FirstOrDefault();
        }

        public IEnumerable<Variant> GetProductVariants(int productId)
        {
            return TestVariants.Variants.Where(v => v.ProductId == productId);
        }

        
    }
}
