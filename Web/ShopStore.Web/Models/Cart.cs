using ShopStore.Services.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopStore.Web.Models
{
    public class Cart
    {
        private List<Guid> _collection = new List<Guid>();

        public Cart()
        { }
        public Cart(IEnumerable<Guid> productIds)
        {
            Collection.AddRange(productIds);
        }

        public void AddProduct(Guid productId)
        {
            if (!_collection.Contains(productId))
            {
                Collection.Add(productId);
            }
        }
        public void RemoveProduct(Guid productId)
        {
            Collection.Remove(productId);
        }
        public void RemoveAll()
        {
            Collection.Clear();
        }

        public int Count { get { return Collection.Count; } }
        public List<Guid> Collection { get { return _collection; } }
    }
}
