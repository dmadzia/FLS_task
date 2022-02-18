using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLS_task.Commerce.Cart.Models
{
    public class CartData
    {
        public double TotalValue { get; set; }
        public IEnumerable<CartLineItem> Items { get; set; }

        public CartData()
        {
            Items = new List<CartLineItem>();
        }
    }
}
