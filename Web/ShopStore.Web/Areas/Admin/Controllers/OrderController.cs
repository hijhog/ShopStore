using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopStore.Services.Data.Interfaces;
using ShopStore.Services.Data.Models;

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
        public IActionResult ChangeOrderStatus(OrderDTO model)
        {
            var result = _orderService.ChangeStatus(model);
            return Json(new { result.Successed, result.Description });
        }

        [HttpGet]
        public IActionResult RemoveOrder(Guid productId, Guid userId)
        {
            var result = _orderService.RemoveOrder(productId, userId);
            return Json(new { result.Successed, result.Description });
        }
    }
}