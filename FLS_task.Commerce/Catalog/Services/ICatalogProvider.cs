using FLS_task.Commerce.Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLS_task.Commerce.Catalog.Services
{
    public interface ICatalogProvider
    {
        IEnumerable<Product> GetProducts();
        Variant GetVariant(string sku);
        IEnumerable<Variant> GetProductVariants(int productId);
    }
}
