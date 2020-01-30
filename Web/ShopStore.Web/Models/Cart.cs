using ShopStore.Services.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopStore.Web.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public void AddProduct(ProductDTO product)
        {
            var cartLine = lineCollection.FirstOrDefault(x=>x.Product.Id == product.Id);
            if(cartLine != null)
            {
                cartLine.Quantity++;
            }
            else
            {
                lineCollection.Add(new CartLine { Product = product, Quantity = 1 });
            }
        } 
        public void RemoveProduct(Guid id)
        {
            var cartLine = lineCollection.FirstOrDefault(x => x.Product.Id == id);
            lineCollection.Remove(cartLine);
        }

        public IEnumerable<Guid> ProductIds { get { return lineCollection.Select(x=>x.Product.Id); } }
        public IEnumerable<CartLine> Collection { get { return lineCollection; } }
        public int Count { get { return lineCollection.Count; } }
    }

    public class CartLine
    {
        public ProductDTO Product { get; set; }
        public int Quantity { get; set; }
    }
}
