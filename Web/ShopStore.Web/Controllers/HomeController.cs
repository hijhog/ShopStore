using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopStore.Services.Contract.Interfaces;
using ShopStore.Web.Filters;

namespace ShopStore.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public HomeController(
            IProductService productService,
            ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [AllowAnonymous]
        [CartFilter]
        public IActionResult Index()
        {
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
    }
}