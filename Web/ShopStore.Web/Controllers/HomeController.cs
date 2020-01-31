using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopStore.Services.Contract.Interfaces;

namespace ShopStore.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public HomeController(
            IProductService productService,
            ICategoryService categoryService,
            ICartService cartService)
            :base(cartService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewBag.ProductCount = GetCart().Count;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetProductsByCategory(Guid categoryId)
        {
            var products = _productService.GetProductsByCategory(categoryId).ToList();
            return Json(new { products });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetCategories()
        {
            var categories = _categoryService.GetAll();
            return Json(new { categories });
        }

        public IActionResult Cart()
        {
            ViewBag.ProductCount = GetCart().Count;
            return View();
        }
    }
}