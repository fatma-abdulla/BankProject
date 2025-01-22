using BankProject.Models;
using BankProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace BankProject.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager)
        {
            signInManager = _signInManager;
            userManager = _userManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.Mobile,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    balance = model.balance
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            return View(model);

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Home", "Account");
                }
                ModelState.AddModelError("", "Invalid user or password");
                return View(model);
            }
            return View(model);
        }

        public async Task<IActionResult> Home()
        {
            var userInfo=await userManager.GetUserAsync(User);
            return View(userInfo);
        }

        public async Task<IActionResult> Deposit(user)
        {
            var role = await userManager.GetUserAsync(User);
            DepositViewModel view = new DepositViewModel { FirstUserId = User., RoleName = role.Name };

            var names = await userManager.GetUsersInRoleAsync(role.Name);

            var users = names.Select(u => u.UserName).ToList();

            var model = new DepositViewModel
            {
FirstUserId = model.
            };
            return View(model);
        }
    }
}
}
