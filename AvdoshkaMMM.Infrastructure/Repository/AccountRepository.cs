using AvdoshkaMMM.Domain.Entities;
using AvdoshkaMMM.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvdoshkaMMM.Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ILogger<AccountRepository> logger;
        public AccountRepository(UserManager<User> _userManager, SignInManager<User> _signInManager, ILogger<AccountRepository> _logger)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            logger = _logger;
        }

        public async Task<User> Login(User user)
        {
            User? loginUser = await userManager.FindByEmailAsync(user.Email);
            if (loginUser == null)
            {
                loginUser = await userManager.FindByNameAsync(user.UserName);
            }
            try
            {
                return loginUser;
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message.ToString(), "$\nОшибка авторизации в репозитории");
                return null;
            }
        }

        public async Task<User> Register(User user, string password)
        {
            User? login = await userManager.FindByEmailAsync(user.UserName);
            User? email = await userManager.FindByEmailAsync(user.Email);
            if(login == null && email == null)
            {
                User registeredUser = new User
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Pathronomic = user.Pathronomic,
                    Email = user.Email,
                };
                IdentityResult? result = await userManager.CreateAsync(registeredUser);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(registeredUser, "User");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        logger.LogError(error.Description);
                    }
                }
            }
            return user;
        }

    }
}
