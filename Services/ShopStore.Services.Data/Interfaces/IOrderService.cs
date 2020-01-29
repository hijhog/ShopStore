using ShopStore.Common;
using ShopStore.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopStore.Services.Data.Interfaces
{
    public interface IOrderService
    {
        IQueryable<OrderDTO> GetOrders();
        IQueryable<OrderDTO> GetUserOrders(Guid userId);
        OperationResult AddOrders(IEnumerable<OrderDTO> orders, Guid userId);
        OperationResult AnnulmentOrder(Guid productId, Guid userId);
        OperationResult RemoveOrder(Guid productId, Guid userId);
        OperationResult ChangeStatus(OrderDTO dto);
    }
}
