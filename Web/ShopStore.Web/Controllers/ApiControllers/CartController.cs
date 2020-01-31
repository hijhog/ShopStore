using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopStore.Services.Contract.Interfaces;
using ShopStore.Services.Contract.Models;
using ShopStore.Web.Extensions;
using ShopStore.Web.Models;

namespace ShopStore.Web.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(
            ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("GetCartProducts")]
        public IActionResult GetCartProducts()
        {
            var cart = _cartService.GetCartByUserId(User.GetUserId());
            return Ok(new { cart });
        }

        [HttpGet("AddProductToCart")]
        public async Task<IActionResult> AddProductToCart(Guid productId)
        {
            CartDTO cartDto = new CartDTO { ProductId = productId, UserId = User.GetUserId() };
            var result = await _cartService.AddProduct(cartDto);
            if (result.Successed)
            {
                var cart = GetCart();
                cart.AddProduct(productId);
                SaveCart(cart);
            }
            return Ok(new { result.Successed, result.Description });
        }

        [HttpGet("RemoveProduct")]
        public async Task<IActionResult> RemoveProduct(Guid productId)
        {
            var result = await _cartService.RemoveProduct(productId, User.GetUserId());
            if (result.Successed)
            {
                var cart = GetCart();
                cart.RemoveProduct(productId);
                SaveCart(cart);
            }
            return Ok(new { result.Successed, result.Description });
        }

        [HttpGet("GetProductCount")]
        public IActionResult GetProductCount()
        {
            var count = _cartService.GetCountProducts(User.GetUserId());
            return Ok(new { count });
        }

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.Get<Cart>("Cart");
            if (cart == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    cart = new Cart(_cartService.GetProductIds(User.GetUserId()));
                }
                else
                {
                    cart = new Cart();
                }
            }
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.Set("Cart", cart);
        }
    }
}
