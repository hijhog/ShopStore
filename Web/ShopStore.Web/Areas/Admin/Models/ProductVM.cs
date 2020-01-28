using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ShopStore.Web.Areas.Admin.Models
{
    public class ProductVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [Display(Name="Category")]
        public Guid CategoryId { get; set; }
        public string Category { get; set; }
        public IFormFile Image { get; set; }
    }
}
