﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopStore.Common;
using ShopStore.Services.Contract.Interfaces;
using ShopStore.Services.Contract.Models;
using ShopStore.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "ManagementAccess")]
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
        [Authorize(Roles = GlobalConstants.Admin)]
        public IActionResult Create()
        {
            var product = new ProductVM();
            var categoriesDTO = _categoryService.GetAll();
            ViewBag.Categories = categoriesDTO.Select(x => _mapper.Map<CategoryVM>(x));
            return View(nameof(GlobalConstants.Details), product);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.Admin)]
        public async Task<IActionResult> Save(ProductVM modelVM)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(GlobalConstants.Details), modelVM);
            }

            ProductDTO productDto = _mapper.Map<ProductDTO>(modelVM);
            if(modelVM.Image != null)
            {
                using(var binaryReader = new BinaryReader(modelVM.Image.OpenReadStream()))
                {
                    productDto.Image = binaryReader.ReadBytes((int)modelVM.Image.Length);
                }
            }
            else
            {
                productDto.Image = null;
            }

            OperationResult result = await _productService.SaveAsync(productDto);
            if (result.Successed)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, result.Description);
            return View(nameof(GlobalConstants.Details), modelVM);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.Admin)]
        public IActionResult Edit(Guid id)
        {
            ProductDTO productDto = _productService.Get(id);
            ProductVM productVM = _mapper.Map<ProductVM>(productDto);
            var categoriesDTO = _categoryService.GetAll();
            ViewBag.Categories = categoriesDTO.Select(x => _mapper.Map<CategoryVM>(x));
            return View(nameof(GlobalConstants.Details), productVM);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.Admin)]
        public async Task<IActionResult> Remove(Guid id)
        {
            var result = await _productService.RemoveAsync(id);
            if (!result.Successed)
            {
                ViewData["ErrorMessage"] = result.Description;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult GetFilterProducts(ProductFilter filter)
        {
            var products = _productService.GetFilterProducts(filter);
            return Json(new { data = products });
        }
    }
}