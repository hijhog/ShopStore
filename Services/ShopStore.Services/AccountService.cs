using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShopStore.Common;
using ShopStore.Data.Models.Interfaces;
using ShopStore.Data.Models.UserEntities;
using ShopStore.Services.Contract.Interfaces;
using ShopStore.Services.Contract.Models;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ShopStore.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountService> _logger;
        public AccountService(
            UserManager<AppUser> userManager,
            RoleManager<Role> roleManager,
            SignInManager<AppUser> signInManager,
            IMapper mapper,
            ILogger<AccountService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OperationResult> LoginAsync(string userName, string password)
        {
            var result = new OperationResult() { Description = "Failed to Log In" };
            try
            {
                AppUser user = await _userManager.FindByNameAsync(userName);
                if(user != null)
                {
                    SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                    if (signInResult.Succeeded)
                    {
                        result.Successed = true;
                    }
                }
                else
                {
                    result.Description = "This user does not exist";
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception: {ex.GetType().ToString()}; Message: {ex.Message}; StackTrace: {ex.StackTrace}");
                result.Description = "Failed to log in";
            }
            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<OperationResult> CreateAsync(UserDTO dto)
        {
            var result = new OperationResult();
            try
            {
                var user = await _userManager.FindByNameAsync(dto.UserName);
                if (user == null)
                {
                    user = _mapper.Map<AppUser>(dto);
                    user.Id = Guid.NewGuid();
                    var identityResult = await _userManager.CreateAsync(user, dto.Password);
                    if (!identityResult.Succeeded)
                    {
                        result.Description = GetErrorMessage(identityResult);
                    }
                    else
                    {
                        var role = await _roleManager.FindByIdAsync(dto.RoleId);
                        identityResult = await _userManager.AddToRoleAsync(user, role.Name);
                        if (!identityResult.Succeeded)
                        {
                            result.Description = GetErrorMessage(identityResult);
                        }
                        else
                        {
                            result.Successed = true;
                        }
                    }
                }
                else
                {
                    result.Description = "User with that UserName already exists!";
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception: {ex.GetType().ToString()}; Message: {ex.Message}; StackTrace: {ex.StackTrace}");
                result.Description = "Failed to create user";
            }            

            return result;
        }

        public async Task<OperationResult> EditAsync(UserDTO dto)
        {
            var result = new OperationResult();
            try 
            {
                var user = await _userManager.FindByIdAsync(dto.Id.ToString());
                if (user != null)
                {
                    _mapper.Map(dto, user);
                    var identityResult = await _userManager.UpdateAsync(user);
                    if (!identityResult.Succeeded)
                    {
                        result.Description = GetErrorMessage(identityResult);
                    }
                    else
                    {
                        result.Successed = true;
                    }
                }
                else
                {
                    result.Description = "This user not exists";
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception: {ex.GetType().ToString()}; Message: {ex.Message}; StackTrace: {ex.StackTrace}");
                result.Description = "Failed to edit user";
            }            

            return result;
        }

        public async Task<OperationResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            var result = new OperationResult();
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId.ToString());
                if (user != null)
                {
                    var identityResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (!identityResult.Succeeded)
                    {
                        result.Description = GetErrorMessage(identityResult);
                    }
                    else
                    {
                        result.Successed = true;
                    }
                }
                else
                {
                    result.Description = "This user not exists";
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception: {ex.GetType().ToString()}; Message: {ex.Message}; StackTrace: {ex.StackTrace}");
                result.Description = "Failed to change password of user";
            }            

            return result;
        }

        private string GetErrorMessage(IdentityResult result)
        {
            StringBuilder errorLine = new StringBuilder();
            foreach (var err in result.Errors)
            {
                errorLine.Append(err.Description);
            }
            return errorLine.ToString();
        }
    }
}
