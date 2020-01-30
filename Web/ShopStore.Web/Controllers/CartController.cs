using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopStore.Services.Contract.Interfaces;
using ShopStore.Services.Contract.Models;
using ShopStore.Web.Extensions;
using ShopStore.Web.Filters;
using ShopStore.Web.Models;

namespace ShopStore.Web.Controllers
{
    public class CartController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        public CartController(
            IProductService productService,
            IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        [CartFilter]
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        [HttpGet]
        public IActionResult AddProductToCart(Guid productId)
        {
            var cart = GetCart();
            var productDto = _productService.Get(productId);
            cart.AddProduct(productDto);
            HttpContext.Session.Set("Cart", cart);
            return Json(new { success = true, productCount = cart.Count });
        }

        [HttpGet]
        public IActionResult RemoveProduct(Guid productId)
        {
            var cart = GetCart();
            cart.RemoveProduct(productId);
            HttpContext.Session.Set("Cart", cart);
            return RedirectToAction(nameof(Index));
        }
    }
}