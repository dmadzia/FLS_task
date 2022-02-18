using FLS_task.Commerce.Cart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLS_task.Commerce.Cart.Repository
{
    public interface ICartRepository
    {
        void SaveCart(CartData cartData);
        CartData GetCart();
    }
}
