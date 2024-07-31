using Juan.Models;
using Juan.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Juan.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<AppUser> users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> ChangeStatus(string id)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null) return BadRequest();
            user.IsBlocked = !user.IsBlocked;
            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(NewUserVM newUser)
        {
            ViewBag.Roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            if (!ModelState.IsValid) return View(newUser);

            AppUser user = new() { Fullname = newUser.Fullname, Email = newUser.Email, UserName = newUser.Username };
            IdentityResult result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(newUser);
            }
            await _userManager.AddToRoleAsync(user, "member");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string? id)
        {
            if (id is null) return BadRequest();
            AppUser? user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            ViewBag.Roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            NewUserVM userVM = new() { Email = user.Email, Fullname = user.Fullname, Username = user.UserName };
            return View(userVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(string? id, NewUserVM userVM)
        {
            if (!ModelState.IsValid) return View(userVM);
            if (id == null) return BadRequest();
            AppUser? user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound();
            ViewBag.Roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            if (userVM.Password != userVM.RePassword)
            {
                ModelState.AddModelError("RePassword", "Passwords do not match");
                return View(userVM);
            }
            if (userVM.Password is not null)
            {

                if (!await _userManager.CheckPasswordAsync(user, userVM.CurrentPassword))
                {
                    ModelState.AddModelError("CurrentPassword", "Wrong Password");
                    return View(userVM);
                }
                await _userManager.ChangePasswordAsync(user, userVM.CurrentPassword, userVM.Password);
            }
            user.UserName = userVM.Username;
            user.Email = userVM.Email;
            user.Fullname = userVM.Fullname;
            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null) return BadRequest();
            AppUser? user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(string? id)
        {
            if (id is null) return BadRequest();
            AppUser? user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        public async Task<IActionResult> ChangeRoles(string? id)
        {
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();
            if (id is null) return BadRequest();
            AppUser? user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            IEnumerable<string> userRoles = await _userManager.GetRolesAsync(user);
            return View(userRoles);
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ChangeRoles(string? id, IEnumerable<string> newUserRoles)
        {
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();
            if (id is null) return BadRequest();
            AppUser? user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            if (newUserRoles.Count() == 0)
            {
                ModelState.AddModelError("", "User must have at least one role");
                return View(newUserRoles);
            }
            foreach (string role in newUserRoles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    ModelState.AddModelError("", "Invalid role");
                    return View(newUserRoles);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
            await _userManager.AddToRolesAsync(user, newUserRoles);
            return RedirectToAction("Index");
        }
    }
}
