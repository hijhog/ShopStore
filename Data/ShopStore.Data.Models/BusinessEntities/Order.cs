using ShopStore.Data.Models.UserEntities;
using System;

namespace ShopStore.Data.Contract.BusinessEntities
{
    public class Order
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public OrderStatus Status { get; set; }
        public int Quantity { get; set; }
        public decimal TotalSum { get; set; }
    }
}
