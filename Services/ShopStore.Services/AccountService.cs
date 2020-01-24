using Microsoft.AspNetCore.Identity;
using ShopStore.Common;
using ShopStore.Data.Identity;
using ShopStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopStore.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<OperationResult> Login(string userName, string password)
        {
            var result = new OperationResult() { Description = "Failed to Log In" };
            try
            {
                AppUser user = await _userManager.FindByNameAsync(userName);
                if(user == null)
                {
                    result.Description = "This user does not exist";
                }
                else
                {
                    SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                    if (signInResult.Succeeded)
                    {
                        result.Successed = true;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Description = ex.Message;
            }
            return result;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
