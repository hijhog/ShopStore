using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopStore.Services.Contract.Interfaces;
using ShopStore.Services.Contract.Models;
using ShopStore.Web.Extensions;
using ShopStore.Web.Filters;
using ShopStore.Web.Models;

namespace ShopStore.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(
            IOrderService orderService,
            ICartService cartService)
            :base(cartService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var orders = _orderService.GetUserOrders(User.GetUserId());
            ViewBag.ProductCount = GetCart().Quantity;
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> MakeOrder()
        {
            var result = await _orderService.MakeAnOrderAsync(User.GetUserId());
            if (result.Successed)
            {
                var cart = GetCart();
                cart.Reset();
                SaveCart(cart);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AnnulmentOrder(Guid productId)
        {
            var result = await _orderService.AnnulmentOrderAsync(productId, User.GetUserId());
            return RedirectToAction(nameof(Index));
        }
    }
}