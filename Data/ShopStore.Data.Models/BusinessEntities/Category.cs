using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Contract.BusinessEntities
{
    public class Category : BaseEntity
    {
        public ICollection<Product> Products { get; set; }
    }
}
