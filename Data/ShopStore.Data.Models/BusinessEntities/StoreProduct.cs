using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Models.BusinessEntities
{
    public class StoreProduct
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid StoreId { get; set; }
        public Store Store { get; set; }

        public int ProductCount { get; set; }
    }
}
