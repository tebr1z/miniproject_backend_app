using Juan.Models;
using Juan.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Juan.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View(register);
            AppUser user = new() { Fullname = register.Fullname, Email = register.Email, UserName = register.Username };
            IdentityResult result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(register);
            }
            await _userManager.AddToRoleAsync(user, "member");
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM, string? returnUrl)
        {
            if (!ModelState.IsValid) return View(loginVM);
            AppUser? user = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError("", "User not found");
                    return View(loginVM);
                }
            }
            bool doesPasswordMatch = await _userManager.CheckPasswordAsync(user, loginVM.Password);
            if (!doesPasswordMatch)
            {
                ModelState.AddModelError("", "Username or password is wrong.");
                return View(loginVM);
            }
            if (doesPasswordMatch && user.IsBlocked)
            {
                ModelState.AddModelError("", "User is blocked.");
                return View(loginVM);
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "User is locked out. Try again lates.");
                return View(loginVM);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "User not found");
                return View(loginVM);
            }
            if (returnUrl is null)
                return RedirectToAction("Index", "Home");
            return Redirect(returnUrl);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
