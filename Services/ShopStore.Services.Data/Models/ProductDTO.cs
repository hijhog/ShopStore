using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Services.Contract.Models
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public string Category { get; set; }
        public byte[] Image { get; set; }
    }
}
