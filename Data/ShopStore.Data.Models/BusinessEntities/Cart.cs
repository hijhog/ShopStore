using ShopStore.Data.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Contract.BusinessEntities
{
    public class Cart
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        public int Quantity { get; set; }
    }
}
