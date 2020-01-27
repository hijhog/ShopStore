using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Services.Data.Models
{
    public class StoreDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProductDTO> Products { get; set; }
    }
}
