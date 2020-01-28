using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopStore.Web.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public void AddProduct(Guid id)
        {
            var cartLine = lineCollection.FirstOrDefault(x=>x.ProductId == id);
            if(cartLine != null)
            {
                cartLine.Quantity++;
            }
            else
            {
                lineCollection.Add(new CartLine { ProductId = id, Quantity = 1 });
            }
        } 
        public void RemoveProduct(Guid id)
        {
            var cartLine = lineCollection.FirstOrDefault(x => x.ProductId == id);
            lineCollection.Remove(cartLine);
        }

        public IEnumerable<Guid> ProductIds { get { return lineCollection.Select(x=>x.ProductId); } }
        public IEnumerable<CartLine> Collection { get { return lineCollection; } }
    }

    public class CartLine
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
