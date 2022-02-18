using FLS_task.Commerce.Catalog.Models;
using FLS_task.Commerce.Catalog.Services;

namespace FLS_task.Features.Catalog.Builders
{
    public class ProductListBuilder
    {
        private readonly ICatalogProvider _catalogProvider;

        public ProductListBuilder(ICatalogProvider catalogProvider)
        {
            _catalogProvider = catalogProvider;
        }

        public IEnumerable<Tuple<Product, IEnumerable<Variant>>> BuildProductList()
        {
            return _catalogProvider.GetProducts().Select(p => new Tuple<Product, IEnumerable<Variant>>(p, _catalogProvider.GetProductVariants(p.Id)));
        }
    }
}
