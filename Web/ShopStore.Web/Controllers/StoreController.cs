using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopStore.Common;
using ShopStore.Services.Data.Interfaces;
using ShopStore.Services.Data.Models;
using ShopStore.Web.Models;

namespace ShopStore.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly IMapper _mapper;
        public StoreController(
            IStoreService storeService,
            IMapper mapper)
        {
            _storeService = storeService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var stores = _storeService.GetAll();
            return View(stores);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var storeVM = new StoreVM();
            return View(nameof(GlobalConstants.Details), storeVM);
        }

        [HttpPost]
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
        public IActionResult Edit(int id)
        {
            StoreDTO storeDto = _storeService.Get(id);
            StoreVM categoryVM = _mapper.Map<StoreVM>(storeDto);
            return View(nameof(GlobalConstants.Details), categoryVM);
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var result = _storeService.Remove(id);
            if (!result.Successed)
            {
                ViewBag.ErrorMessage = result.Description;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}