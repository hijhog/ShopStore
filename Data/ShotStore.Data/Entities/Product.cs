using System;
using System.Collections.Generic;
using System.Text;

namespace ShotStore.Data.Entities
{
    public class Product : BaseEntity
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Store> Stores { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
