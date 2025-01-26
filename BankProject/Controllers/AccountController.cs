using BankProject.Data;
using BankProject.Models;
using BankProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BankProject.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private ApplicationDbContext db;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AccountController(UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager,
            ApplicationDbContext _db,
            IWebHostEnvironment hostEnvironment)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            db = _db;
            webHostEnvironment = hostEnvironment;
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
                string? uniqueFileName = UploadedFile(model);
                ApplicationUser user = new ApplicationUser()
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.Mobile,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    balance = model.balance,
                    ProfilePicture ="~/img/"+ uniqueFileName
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
        private string UploadedFile(RegisterViewModel model)
        {
            string uniqueFileName = null;
            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
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
            var userInfo = await userManager.GetUserAsync(User);
            return View(userInfo);
        }
        [HttpGet]
        public async Task<IActionResult> Deposit()
        {
            var user = await userManager.GetUserAsync(User);

            DepositViewModel model = new DepositViewModel
            {
                FirstUserId = user.Id,
                balance = user.balance,
                Newbalance = 0
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Deposit(DepositViewModel model)
        {

            var user = await userManager.FindByIdAsync(model.FirstUserId);

            var transaction = new Transactions
            {
                UserId = user.Id,
                OldBalance = model.balance,
                UpdeatedBalance = user.balance + model.Newbalance,
                TransationDate = DateTime.Now,
                TransactionType = "Deposit",
                Newbalance = model.Newbalance
            };

            user.balance += model.Newbalance;
            await userManager.UpdateAsync(user);

            db.Add(transaction);
            await db.SaveChangesAsync();

            return RedirectToAction("Home");

        }

        [HttpGet]
        public async Task<IActionResult> Withdraw()
        {
            var user = await userManager.GetUserAsync(User);

            DepositViewModel model = new DepositViewModel
            {
                FirstUserId = user.Id,
                balance = user.balance,
                Newbalance = 0
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Withdraw(DepositViewModel model)
        {

            var user = await userManager.FindByIdAsync(model.FirstUserId);

            var transaction = new Transactions
            {
                UserId = user.Id,
                OldBalance = model.balance,
                UpdeatedBalance = user.balance + model.Newbalance,
                TransationDate = DateTime.Now,
                TransactionType = "Withdraw",
                                Newbalance = model.Newbalance

            };

            user.balance -= model.Newbalance;
            await userManager.UpdateAsync(user);

            db.Add(transaction);
            await db.SaveChangesAsync();

            return RedirectToAction("Home");
        }
        [HttpGet]
        public async Task<IActionResult> Logout(LoginViewModel model)
        {

            return RedirectToAction("Register", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Register", "Account");
        }
        public async Task<IActionResult> Transactions()
        {
            var user = await userManager.GetUserAsync(User);
            var model = new TransactionViewModel
            {
                UserName = user.Name,
                Transactions = await db.Transactions.Where(x => x.UserId == user.Id).ToListAsync()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Transactions(string ttype, decimal byAmount)
        {
            var user = await userManager.GetUserAsync(User);
            var model = new TransactionViewModel
            {
                UserName = user.Name,
                Transactions = await db.Transactions.Where(x => x.TransactionType == ttype).ToListAsync()
            };
            if (ttype=="All")
            {
                 model = new TransactionViewModel
                {
                    UserName = user.Name,
                    Transactions = await db.Transactions.Where(x => x.UserId == user.Id).ToListAsync()
                };
            }
            if (byAmount != 0)
            {
                model = new TransactionViewModel
                {
                    UserName = user.Name,
                    Transactions = await db.Transactions.Where(x => x.Newbalance == byAmount).ToListAsync()
                };
            }
            return View(model);
        }
        public async Task<IActionResult> AllUsers()
        {
            var CurrentUser = await userManager.GetUserAsync(User);
            var users = await userManager.Users.Where(user=>user.Id != CurrentUser.Id).ToListAsync();
            ViewBag.MyBalance = CurrentUser.balance;
            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            return View(user);
        }
        public async Task<IActionResult> Transfer(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            TransferViewModel model = new TransferViewModel
            {
                FirstUserId = user.Id,
                CurrentBalance = user.balance,
            };

            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> Transfer(TransferViewModel model)
        {
            var user = await userManager.GetUserAsync(User);
            var first = await userManager.FindByIdAsync(model.FirstUserId);
             var second = await userManager.FindByIdAsync(model.SecondUserId);


            user.balance -= model.TransferredAmount;
            first.balance += model.TransferredAmount;
            await userManager.UpdateAsync(first);
            await userManager.UpdateAsync(user);
            var firsttrans = new Transactions
            {
                UserId = user.Id,
                OldBalance = user.balance,
                UpdeatedBalance = user.balance - model.TransferredAmount,
                TransationDate = DateTime.Now,
                TransactionType = "Transferred Money",
                Newbalance = model.TransferredAmount
                

            };
            var secondtrans = new Transactions
            {
                UserId = first.Id,
                OldBalance = first.balance,
                UpdeatedBalance = first.balance + model.TransferredAmount,
                TransationDate = DateTime.Now,
                TransactionType = "Recieved Money",
                Newbalance = model.TransferredAmount
            };
            db.Add(firsttrans);
            await db.SaveChangesAsync();
            db.Add(secondtrans);
            await db.SaveChangesAsync();

            return RedirectToAction("AllUsers");
        }

        
        public async Task<IActionResult> Profile()
        {
            var user = await userManager.GetUserAsync(User);

            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Name);
                user.Name = model.Name;
                user.Gender = model.Gender;
                user.DateOfBirth = model.DateOfBirth;
                user.PhoneNumber = model.Mobile;
                //continue name and pohone and the rest 
                await userManager.UpdateAsync(user);
                return RedirectToAction("RolesList");
            };
            return View(model);
        }

    }
}
