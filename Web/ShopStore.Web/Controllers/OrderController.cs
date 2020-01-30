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
            IOrderService orderService)
        {
            _orderService = orderService;
        }

        [CartFilter]
        public IActionResult Index()
        {
            var orders = _orderService.GetUserOrders(User.GetUserId());
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> MakeOrder()
        {
            var cart = GetCart();
            var orders = cart.Collection.Select(x => new OrderDTO { ProductId = x.Product.Id, Quantity = x.Quantity });
            var result = await _orderService.AddOrdersAsync(orders, User.GetUserId());
            if (result.Successed)
            {
                HttpContext.Session.Set("Cart", new Cart());
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