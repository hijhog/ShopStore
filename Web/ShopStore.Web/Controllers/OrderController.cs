using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopStore.Services.Contract.Interfaces;
using ShopStore.Web.Extensions;

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
            ViewBag.ProductCount = GetCart().Count;
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> MakeOrder()
        {
            var result = await _orderService.MakeAnOrderAsync(User.GetUserId());
            if (result.Successed)
            {
                var cart = GetCart();
                cart.RemoveAll();
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