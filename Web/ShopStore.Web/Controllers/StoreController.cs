using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopStore.Services.Data.Interfaces;

namespace ShopStore.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly IMapper mapper;
        public StoreController(
            IStoreService storeService,
            IMapper mapper)
        {

        }
        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    var storeVM = new StoreVM();
        //    return View(nameof(GlobalConstants.Details), storeVM);
        //}

        //[HttpPost]
        //public IActionResult Save(StoreVM modelVM)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(nameof(GlobalConstants.Details), modelVM);
        //    }

        //    CategoryDTO categoryDto = _mapper.Map<CategoryDTO>(modelVM);
        //    OperationResult result = _categoryService.Save(categoryDto);
        //    if (result.Successed)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }

        //    ModelState.AddModelError(string.Empty, result.Description);
        //    return View(nameof(GlobalConstants.Details), modelVM);
        //}

        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    CategoryDTO categoryDto = _categoryService.Get(id);
        //    StoreVM categoryVM = _mapper.Map<StoreVM>(categoryDto);
        //    return View(nameof(GlobalConstants.Details), categoryVM);
        //}

        //[HttpGet]
        //public IActionResult Remove(int id)
        //{
        //    var result = _categoryService.Remove(id);
        //    if (!result.Successed)
        //    {
        //        ViewBag.ErrorMessage = result.Description;
        //    }

        //    return RedirectToAction(nameof(Index));
        //}
    }
}