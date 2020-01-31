using AutoMapper;
using ShopStore.Common;
using ShopStore.Data.Contract.BusinessEntities;
using ShopStore.Data.Models.Interfaces;
using ShopStore.Services.Contract.Interfaces;
using ShopStore.Services.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopStore.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IMapper _mapper;
        public CartService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cartRepository = unitOfWork.GetRepository<Cart>();
            _mapper = mapper;
        }

        public IEnumerable<CartDTO> GetCartByUserId(Guid userId)
        {
            return _cartRepository.IncludeMultiple(_cartRepository.GetAll().Where(x => x.UserId == userId), p => p.Product)
                .Select(x => _mapper.Map<CartDTO>(x));
        }

        public async Task<OperationResult> AddProduct(CartDTO cartDto)
        {
            var result = new OperationResult();
            try
            {
                Cart cart = _cartRepository.Get(cartDto.ProductId, cartDto.UserId);
                if(cart != null)
                {
                    cart.Quantity++;
                }
                else
                {
                    cart = _mapper.Map<Cart>(cartDto);
                    cart.Quantity = 1;
                    _cartRepository.Insert(cart);
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

        public async Task<OperationResult> RemoveProduct(Guid productId, Guid userId)
        {
            var result = new OperationResult();
            try
            {
                _cartRepository.Remove(productId, userId);
                await _unitOfWork.SaveAsync();
                result.Successed = true;
            }
            catch(Exception ex)
            {
                result.Description = ex.Message;
            }

            return result;
        }

        public async Task<OperationResult> RemoveCartUser(Guid userId)
        {
            var result = new OperationResult();
            try
            {
                var carts = _cartRepository.GetAll().Where(x => x.UserId == userId);
                foreach(var cart in carts)
                {
                    _cartRepository.Remove(cart.ProductId, cart.UserId);
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

        public int GetCountProducts(Guid userId)
        {
            return _cartRepository.GetAll().Where(x => x.UserId == userId).Count();
        }

        public IEnumerable<Guid> GetProductIds(Guid userId)
        {
            return _cartRepository.GetAll().Where(x => x.UserId == userId).Select(x => x.ProductId);
        }
    }
}
