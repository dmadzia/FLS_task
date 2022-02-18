using FLS_task.Commerce.Catalog.Models;

namespace FLS_task.Features.Catalog.ViewModels
{
    public class ProductListPageViewModel
    {
        public IEnumerable<Tuple<Product, IEnumerable<Variant>>> Products { get; set; }

        public ProductListPageViewModel(IEnumerable<Tuple<Product, IEnumerable<Variant>>> products)
        {
            Products = products;
        }
    }
}
