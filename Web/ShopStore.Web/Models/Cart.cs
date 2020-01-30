using ShopStore.Services.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopStore.Web.Models
{
    public class Cart
    {
        public int Quantity { get; set; }
        public void Reset()
        {
            Quantity = 0;
        }
        public static Cart operator ++(Cart a)
        {
            a.Quantity += 1;
            return a;
        }
        public static Cart operator --(Cart a)
        {
            a.Quantity -= 1;
            return a;
        }
    }
}
