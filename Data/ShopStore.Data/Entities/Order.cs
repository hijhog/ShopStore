using ShopStore.Data.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Entities
{
    public class Order
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }

        public OrderStatus Status { get; set; }
    }
}
