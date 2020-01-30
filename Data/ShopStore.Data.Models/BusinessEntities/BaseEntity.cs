using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Contract.BusinessEntities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
