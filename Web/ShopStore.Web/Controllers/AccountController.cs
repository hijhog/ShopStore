using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopStore.Common;
using ShopStore.Services.Contract.Interfaces;
using ShopStore.Services.Contract.Models;
using ShopStore.Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ShopStore.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public AccountController(
            IAccountService accountService,
            IRoleService roleService,
            IMapper mapper)
        {
            _accountService = accountService;
            _roleService = roleService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model, string returnUrl = null)
        {
            this.ViewData["ReturnUrl"] = returnUrl;
            if (this.ModelState.IsValid)
            {
                var result = await _accountService.LoginAsync(model.UserName, model.Password);
                if (result.Successed)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    this.ModelState.AddModelError("LogInFailed", result.Description);
                    return this.View(model);
                }
            }

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userVM = new UserVM();
            if (!User.IsInRole("Admin"))
            {
                userVM.RoleId = (await _roleService.GetRoleIdByNameAsync(GlobalConstants.User)).Id.ToString();
            }
            else
            {
                ViewBag.Roles = _roleService.GetRoles().Select(x => new SelectListItem(x.Name, x.Id.ToString()));
            }

            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _roleService.GetRoles().Select(x => new SelectListItem(x.Name, x.Id.ToString()));
                return View(model);
            }

            var userDTO = _mapper.Map<UserDTO>(model);
            var result = await _accountService.CreateAsync(userDTO);
            if (result.Successed)
            {
                return RedirectToAction(nameof(Login));
            }

            ViewBag.Roles = _roleService.GetRoles().Select(x => new SelectListItem(x.Name, x.Id.ToString()));
            ModelState.AddModelError(string.Empty, result.Description);
            return View(model);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}