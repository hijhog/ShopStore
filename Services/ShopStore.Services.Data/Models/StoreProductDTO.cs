using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Services.Data.Models
{
    public class StoreProductDTO
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public int ProductCount { get; set; }
        public bool IsExistInStore { get; set; }
    }
}
