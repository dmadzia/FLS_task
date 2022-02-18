using FLS_task.Commerce.Catalog.Services;
using FLS_task.Features.Catalog.Builders;
using FLS_task.Features.Catalog.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FLS_task.Features.Catalog.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogProvider _catalogProvider;

        public CatalogController(ICatalogProvider catalogProvider)
        {
            _catalogProvider = catalogProvider;
        }

        public IActionResult Index()
        {
            var vm = new ProductListPageViewModel(new ProductListBuilder(_catalogProvider).BuildProductList());
            return View(vm);
        }
    }
}
