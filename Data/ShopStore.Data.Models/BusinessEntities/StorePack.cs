using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Models.BusinessEntities
{
    public class StorePack
    {
        public int PackId { get; set; }
        public Pack Pack { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }
    }
}
