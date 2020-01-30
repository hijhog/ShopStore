using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopStore.Services.Contract.Interfaces;
using ShopStore.Web.Extensions;
using ShopStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopStore.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private readonly ICartService _cartService;
        public BaseController(ICartService cartService)
        {
            _cartService = cartService;
        }
        protected Cart GetCart()
        {
            Cart cart = HttpContext.Session.Get<Cart>("Cart");
            if (cart == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    cart = new Cart { Quantity = _cartService.GetCountProducts(User.GetUserId()) };
                }
                else
                {
                    cart = new Cart();
                }
            }
            return cart;
        }

        protected void SaveCart(Cart cart)
        {
            HttpContext.Session.Set("Cart", cart);
        }
    }
}
