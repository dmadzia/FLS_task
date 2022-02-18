using FLS_task.Commerce.Cart.Models;
using FLS_task.Commerce.Discounts.Models;
using FLS_task.Commerce.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLS_task.Commerce.Discounts.Services
{
    public class TestDiscountsProvider : IDiscountsProvider
    {
        public IEnumerable<Discount> GetDiscounts(string sku)
        {
            return TestDiscounts.Discounts.Where(d => d.SKU == sku);
        }

        public IEnumerable<T> GetDiscounts<T>(string sku) where T : Discount
        {
            return TestDiscounts.Discounts.Where(d => d.SKU == sku && d is T).Select(d => (T)d);
        }
    }
}
