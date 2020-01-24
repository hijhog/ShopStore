using ShopStore.Data.Models.UserEntities;

namespace ShopStore.Data.Models.BusinessEntities
{
    public class Order
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }

        public OrderStatus Status { get; set; }
    }
}
