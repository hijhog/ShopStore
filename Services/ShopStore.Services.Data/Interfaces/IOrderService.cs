using ShopStore.Common;
using ShopStore.Services.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopStore.Services.Contract.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderDTO> GetOrders();
        IEnumerable<OrderDTO> GetUserOrders(Guid userId);
        Task<OperationResult> MakeAnOrderAsync(Guid userId);
        Task<OperationResult> AnnulmentOrderAsync(Guid productId, Guid userId);
        Task<OperationResult> RemoveOrderAsync(Guid productId, Guid userId);
        Task<OperationResult> ChangeStatusAsync(OrderDTO dto);
    }
}
