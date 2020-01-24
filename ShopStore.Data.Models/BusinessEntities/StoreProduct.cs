using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Models.BusinessEntities
{
    public class StoreProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }
    }
}
