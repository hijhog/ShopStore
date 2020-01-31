using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopStore.Services.Contract.Interfaces;
using ShopStore.Services.Contract.Models;
using ShopStore.Web.Extensions;

namespace ShopStore.Web.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;
        public CartController(
            ICartService cartService)
            :base(cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            ViewBag.ProductCount = GetCart().Count;
            return View();
        }

        [HttpGet]
        public IActionResult GetCartProducts()
        {
            var cart = _cartService.GetCartByUserId(User.GetUserId());
            return Json(new { cart });
        }

        [HttpGet]
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
            return Json(new { result.Successed, result.Description });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveProduct(Guid productId)
        {
            var result = await _cartService.RemoveProduct(productId, User.GetUserId());
            if(result.Successed)
            {
                var cart = GetCart();
                cart.RemoveProduct(productId);
                SaveCart(cart);
            }
            return Json(new { result.Successed, result.Description });
        }

        [HttpGet]
        public IActionResult GetProductCount()
        {
            var count = _cartService.GetCountProducts(User.GetUserId());
            return Json(new { count });
        }
    }
}