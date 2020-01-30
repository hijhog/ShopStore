using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Services.Contract.Models
{
    public class CartDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public byte[] ProductImage { get; set; }
        public decimal Price { get; set; }
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
    }
}
