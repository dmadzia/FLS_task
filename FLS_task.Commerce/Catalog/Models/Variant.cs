using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLS_task.Commerce.Catalog.Models
{
    public class Variant
    {
        public int ProductId { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
