using AvdoshkaMMM.Domain.Entities;
using AvdoshkaMMM.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

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

        public async Task<bool> Login(User user)
        {
            User? loginUser = await userManager.FindByEmailAsync(user.UserName);
            if (loginUser == null)
            {
                loginUser = await userManager.FindByNameAsync(user.UserName);
                if(loginUser == null)
                {
                    return false;
                }
                var result = await signInManager.PasswordSignInAsync(loginUser, user.PasswordHash, isPersistent: false, lockoutOnFailure:false);
                return result.Succeeded;
            }
            return false;
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
                return user;
            }
            else
            {
                logger.LogWarning("Пользователь существует(репозиторий)");
                return null;
            }
        }

    }
}
