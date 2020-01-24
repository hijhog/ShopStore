using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Entities
{
    public class Store : BaseEntity
    {
        public ICollection<StoreProduct> StoreProducts { get; set; }
    }
}
