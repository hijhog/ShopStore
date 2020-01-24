using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopStore.Common;
using ShopStore.Services.Interfaces;
using ShopStore.Services.Models;
using ShopStore.Web.Models;

namespace ShopStore.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(
            ICategoryService categoryService,
            IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categoryVM = new CategoryVM();
            return View(nameof(GlobalConstants.Details), categoryVM);
        }

        [HttpPost]
        public IActionResult Save(CategoryVM modelVM)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(GlobalConstants.Details), modelVM);
            }

            CategoryDTO categoryDto = _mapper.Map<CategoryDTO>(modelVM);
            OperationResult result = _categoryService.Save(categoryDto);
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
            CategoryDTO categoryDto = _categoryService.Get(id);
            CategoryVM categoryVM = _mapper.Map<CategoryVM>(categoryDto);
            return View(nameof(GlobalConstants.Details), categoryVM);
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var result = _categoryService.Remove(id);
            if (!result.Successed)
            {
                ViewBag.ErrorMessage = result.Description;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}