using FLS_task.Commerce.Discounts.Models;

namespace FLS_task.Commerce.Discounts.Services
{
    public interface IDiscountsProvider
    {
        IEnumerable<Discount> GetDiscounts(string sku);
        IEnumerable<T> GetDiscounts<T>(string sku) where T : Discount;
    }
}
