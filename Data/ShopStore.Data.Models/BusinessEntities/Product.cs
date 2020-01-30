using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Contract.BusinessEntities
{
    public class Product : BaseEntity
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public byte[] Image { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Cart> Cart { get; set; }
    }
}
