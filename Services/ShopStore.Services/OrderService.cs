using ShopStore.Data.Models.Interfaces;
using ShopStore.Services.Contract.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
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
        private readonly IMapper _mapper;
        public OrderService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<OrderDTO> GetOrders()
        {
            var orders = from order in _unitOfWork.OrderRepository.GetAll()
                         join product in _unitOfWork.ProductRepository.GetAll()
                         on order.ProductId equals product.Id
                         join user in _unitOfWork.UserRepository.GetAll()
                         on order.UserId equals user.Id
                         select new OrderDTO
                         {
                             ProductId = product.Id,
                             ProductName = product.Name,
                             Quantity = order.Quantity,
                             Status = order.Status,
                             StatusText = order.Status.ToString(),
                             TotalSum = order.TotalSum,
                             UserId = user.Id,
                             FullName = $"{user.LastName} {user.FirstName} {user.Patronymic}"
                         };
            return orders;
        }

        public IEnumerable<OrderDTO> GetUserOrders(Guid userId)
        {
            var orders = from order in _unitOfWork.OrderRepository.GetAll()
                         join product in _unitOfWork.ProductRepository.GetAll()
                         on order.ProductId equals product.Id
                         where order.UserId == userId
                         select new OrderDTO
                         {
                             ProductId = product.Id,
                             ProductName = product.Name,
                             ProductImage = product.Image,
                             Price = product.Price,
                             Status = order.Status,
                             StatusText = order.Status.ToString(),
                             Quantity = order.Quantity
                         };
            return orders;
        }

        public async Task<OperationResult> AddOrdersAsync(IEnumerable<OrderDTO> orders, Guid userId)
        {
            var result = new OperationResult();
            try
            {
                foreach(var o in orders)
                {
                    var product = _unitOfWork.ProductRepository.Get(o.ProductId);
                    var order = _mapper.Map<Order>(o);
                    order.UserId = userId;
                    order.Status = OrderStatus.Accepted;
                    order.TotalSum = product.Price * o.Quantity;

                    _unitOfWork.OrderRepository.Insert(order);
                }

                await _unitOfWork.SaveAsync();
                result.Successed = true;
            }
            catch(Exception ex)
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
                var order = _unitOfWork.OrderRepository.Get(productId, userId);
                order.Status = OrderStatus.Rejected;
                _unitOfWork.OrderRepository.Update(order);
                await _unitOfWork.SaveAsync();
                result.Successed = true;
            }
            catch(Exception ex)
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
                _unitOfWork.OrderRepository.Remove(productId, userId);
                await _unitOfWork.SaveAsync();

                result.Successed = true;
            }
            catch(Exception ex)
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
                var order = _unitOfWork.OrderRepository.Get(dto.ProductId, dto.UserId);
                order.Status = dto.Status;
                _unitOfWork.OrderRepository.Update(order);
                await _unitOfWork.SaveAsync();
                result.Successed = true;
            }
            catch(Exception ex)
            {
                result.Description = ex.Message;
            }
            return result;
        }
    }
}
