using ShopStore.Data.Contract.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Services.Contract.Models
{
    public class OrderDTO
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalSum { get; set; }
        public byte[] ProductImage { get; set; }
        public string FullName { get; set; }
        public OrderStatus Status { get; set; }
        public string StatusText { get; set; }
    }
}
