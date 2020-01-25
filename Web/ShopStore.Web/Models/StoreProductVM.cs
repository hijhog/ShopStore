using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopStore.Web.Models
{
    public class StoreProductVM
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int ProductCount { get; set; }
        public bool IsExistInStore { get; set; }
    }
}
