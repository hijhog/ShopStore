using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopStore.Common;
using ShopStore.Services.CategoryService.Interfaces;
using ShopStore.Services.ProductService.Interfaces;
using ShopStore.Services.ProductService.Models;
using ShopStore.Web.Models;

namespace ShopStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAll();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var product = new ProductVM();
            var categoriesDTO = _categoryService.GetAll();
            ViewBag.Categories = categoriesDTO.Select(x => _mapper.Map<CategoryVM>(x));
            return View(nameof(GlobalConstants.Details), product);
        }

        [HttpPost]
        public IActionResult Save(ProductVM modelVM)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(GlobalConstants.Details), modelVM);
            }

            ProductDTO productDto = _mapper.Map<ProductDTO>(modelVM);
            OperationResult result = _productService.Save(productDto);
            if (result.Successed)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, result.Description);
            return View(nameof(GlobalConstants.Details), modelVM);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ProductDTO productDto = _productService.Get(id);
            ProductVM productVM = _mapper.Map<ProductVM>(productDto);
            return View(nameof(GlobalConstants.Details), productVM);
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var result = _categoryService.Remove(id);
            if (!result.Successed)
            {
                ViewData["ErrorMessage"] = result.Description;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}