using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AccountService(
            UserManager<AppUser> userManager,
            RoleManager<Role> roleManager,
            SignInManager<AppUser> signInManager,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                result.Description = ex.Message;
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
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if(user == null)
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

            return result;
        }

        public async Task<OperationResult> EditAsync(UserDTO dto)
        {
            var result = new OperationResult();
            var user = _unitOfWork.UserRepository.Get(dto.Id);
            if(user != null)
            {
                _mapper.Map(dto, user);
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.SaveAsync();
                result.Successed = true;
            }
            else
            {
                result.Description = "This user not exists";
            }

            return result;
        }

        public async Task<OperationResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            var result = new OperationResult();
            var user = _unitOfWork.UserRepository.Get(model.UserId);
            if (user != null)
            {
                var identityResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (!identityResult.Succeeded)
                {
                    StringBuilder errorLine = new StringBuilder();
                    foreach(var err in identityResult.Errors)
                    {
                        errorLine.Append(err.Description);
                    }
                    result.Description = errorLine.ToString();
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
