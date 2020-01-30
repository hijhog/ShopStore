using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopStore.Services.Contract.Interfaces;
using ShopStore.Services.Contract.Models;

namespace ShopStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "ManagementAccess")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(
            IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _orderService.GetOrders();
            return Json(new { data = orders });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeOrderStatus(OrderDTO model)
        {
            var result = await _orderService.ChangeStatusAsync(model);
            return Json(new { result.Successed, result.Description });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveOrder(Guid productId, Guid userId)
        {
            var result = await _orderService.RemoveOrderAsync(productId, userId);
            return Json(new { result.Successed, result.Description });
        }
    }
}