using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopStore.Services.Data.Interfaces;
using ShopStore.Web.Extensions;
using ShopStore.Web.Models;

namespace ShopStore.Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        public CartController(
            IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            var cart = GetCart();
            var products = _productService.GetFilteredProducts(cart.ProductIds);
            return View(products);
        }

        [HttpGet]
        public IActionResult AddProductToCart(Guid productId)
        {
            var cart = GetCart();
            cart.AddProduct(productId);
            HttpContext.Session.Set("Cart", cart);
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult RemoveProduct(Guid productId)
        {
            var cart = GetCart();
            cart.RemoveProduct(productId);
            HttpContext.Session.Set("Cart", cart);
            return RedirectToAction(nameof(Index));
        }

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.Get<Cart>("Cart");
            if (cart == null)
            {
                cart = new Cart();
            }
            return cart;
        }
    }
}