﻿using AutoMapper;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CartService> _logger;
        public CartService(
            IUnitOfWork unitOfWork,
            IMapper mapper, ILogger<CartService> logger)
        {
            _unitOfWork = unitOfWork;
            _cartRepository = unitOfWork.GetRepository<Cart>();
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<CartDTO> GetCartByUserId(Guid userId)
        {
            return _mapper.ProjectTo<CartDTO>(_cartRepository.GetAll().Where(x => x.UserId == userId));
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
                _logger.LogError($"Exception: {ex.GetType().ToString()}; Message: {ex.Message}; StackTrace: {ex.StackTrace}");
                result.Description = "Failed to add product";
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
                _logger.LogError($"Exception: {ex.GetType().ToString()}; Message: {ex.Message}; StackTrace: {ex.StackTrace}");
                result.Description = "Failed to remove product";
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
                _logger.LogError($"Exception: {ex.GetType().ToString()}; Message: {ex.Message}; StackTrace: {ex.StackTrace}");
                result.Description = "Failed to remove products in the cart";
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
