using ShopStore.Data.Models.Interfaces;
using ShopStore.Services.Contract.Models;
using System.Linq;
using System.Collections.Generic;
using ShopStore.Common;
using System;
using AutoMapper;
using ShopStore.Data.Contract.BusinessEntities;
using ShopStore.Services.Contract.Interfaces;
using System.Threading.Tasks;

namespace ShopStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper _mapper;
        public OrderService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = unitOfWork.GetRepository<Order>();
            _mapper = mapper;
        }

        public IEnumerable<OrderDTO> GetOrders()
        {
            var orders = _orderRepository.IncludeMultiple(_orderRepository.GetAll(), p => p.Product, p => p.User);
            return orders.Select(x => _mapper.Map<OrderDTO>(x));
        }

        public IEnumerable<OrderDTO> GetUserOrders(Guid userId)
        {
            var orders = _orderRepository.IncludeMultiple(_orderRepository.GetAll().Where(x => x.UserId == userId), p => p.Product, p => p.User);
            return orders.Select(x => _mapper.Map<OrderDTO>(x));
        }

        public async Task<OperationResult> AddOrdersAsync(IEnumerable<OrderDTO> orders, Guid userId)
        {
            var result = new OperationResult();
            try
            {
                foreach (var o in orders)
                {
                    var product = _unitOfWork.GetRepository<Product>().Get(o.ProductId);
                    var order = _mapper.Map<Order>(o);
                    order.UserId = userId;
                    order.Status = OrderStatus.Accepted;
                    order.TotalSum = product.Price * o.Quantity;

                    _orderRepository.Insert(order);
                }

                await _unitOfWork.SaveAsync();
                result.Successed = true;
            }
            catch (Exception ex)
            {
                result.Description = ex.Message;
            }
            return result;
        }

        public async Task<OperationResult> AnnulmentOrderAsync(Guid productId, Guid userId)
        {
            var result = new OperationResult();
            try
            {
                var order = _orderRepository.Get(productId, userId);
                order.Status = OrderStatus.Rejected;
                _orderRepository.Update(order);
                await _unitOfWork.SaveAsync();
                result.Successed = true;
            }
            catch (Exception ex)
            {
                result.Description = ex.Message;
            }
            return result;
        }

        public async Task<OperationResult> RemoveOrderAsync(Guid productId, Guid userId)
        {
            var result = new OperationResult();
            try
            {
                _orderRepository.Remove(productId, userId);
                await _unitOfWork.SaveAsync();

                result.Successed = true;
            }
            catch (Exception ex)
            {
                result.Description = ex.Message;
            }

            return result;
        }

        public async Task<OperationResult> ChangeStatusAsync(OrderDTO dto)
        {
            var result = new OperationResult();
            try
            {
                var order = _orderRepository.Get(dto.ProductId, dto.UserId);
                order.Status = dto.Status;
                _orderRepository.Update(order);
                await _unitOfWork.SaveAsync();
                result.Successed = true;
            }
            catch (Exception ex)
            {
                result.Description = ex.Message;
            }
            return result;
        }
    }
}
