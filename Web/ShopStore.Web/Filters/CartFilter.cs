using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using ShopStore.Web.Extensions;
using ShopStore.Web.Models;
using System;

namespace ShopStore.Web.Filters
{
    public class CartFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;
            Cart cart = context.HttpContext.Session.Get<Cart>("Cart") ?? new Cart();

            controller.ViewBag.ProductCount = cart.Count;
        }
    }
}
