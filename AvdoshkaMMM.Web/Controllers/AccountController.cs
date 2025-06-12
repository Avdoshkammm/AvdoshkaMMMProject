using AvdoshkaMMM.Application.DTO;
using AvdoshkaMMM.Application.Interfaces;
using AvdoshkaMMM.Domain.Entities;
using AvdoshkaMMM.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace AvdoshkaMMM.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> logger;
        private readonly SignInManager<User> sim;
        private readonly IAccountService accountService;
        private readonly UserManager<User> um;
        public AccountController(ILogger<AccountController> _logger, SignInManager<User> _sim, IAccountService _accountService, UserManager<User> _um)
        {
            sim = _sim;
            logger = _logger;
            accountService = _accountService;
            um = _um;
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel vm = new();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if(ModelState.IsValid)
            {
                UserDTO user = new UserDTO
                {
                    UserName = vm.Login,
                    Password = vm.Password
                };
                var sussess = await accountService.Login(user);
                if (sussess)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "");
            }
            return View(vm);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginViewModel vm)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User dbUser = new User
        //        {
        //            UserName = vm.Login
        //        };
        //        var user = await sim.
        //    }
        //}
    }
}
