using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopStore.Common;
using ShopStore.Services.Data.Interfaces;
using ShopStore.Services.Data.Models;
using ShopStore.Web.Areas.Admin.Models;
using System;
using System.Linq;

namespace ShopStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "ManagementAccess")]
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public StoreController(
            IStoreService storeService,
            IProductService productService,
            IMapper mapper)
        {
            _storeService = storeService;
            _productService = productService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var stores = _storeService.GetAll();
            return View(stores);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.Admin)]
        public IActionResult Create()
        {
            var storeVM = new StoreVM();
            return View(nameof(GlobalConstants.Details), storeVM);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.Admin)]
        public IActionResult Save(StoreVM modelVM)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(GlobalConstants.Details), modelVM);
            }

            StoreDTO storeDto = _mapper.Map<StoreDTO>(modelVM);
            OperationResult result = _storeService.Save(storeDto);
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
            StoreDTO storeDto = _storeService.Get(id);
            StoreVM categoryVM = _mapper.Map<StoreVM>(storeDto);
            return View(nameof(GlobalConstants.Details), categoryVM);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.Admin)]
        public IActionResult Remove(Guid id)
        {
            var result = _storeService.Remove(id);
            if (!result.Successed)
            {
                ViewBag.ErrorMessage = result.Description;
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ProductList(Guid storeId)
        {
            ViewBag.StoreId = storeId;
            return View();
        }

        [HttpGet]
        public IActionResult GetStoreProducts(Guid storeId)
        {
            var storeProductDTOs = _storeService.GetStoreProducts(storeId);
            var storeProductVMs = storeProductDTOs.Select(x => _mapper.Map<StoreProductVM>(x));
            return Json(new { data = storeProductVMs });
        }

        [HttpPost]
        public JsonResult AddProduct(StoreProductVM modelVM)
        {
            var storeProductDTO = _mapper.Map<StoreProductDTO>(modelVM);
            var result = _storeService.AddProduct(storeProductDTO);
            return Json(new { result.Successed, result.Description });
        }

        [HttpGet]
        public JsonResult RemoveProduct(Guid productId, Guid storeId)
        {
            var result = _storeService.RemoveProduct(productId, storeId);
            return Json(new { result.Successed, result.Description });
        }
    }
}