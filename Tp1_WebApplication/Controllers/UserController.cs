using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tp1_CoreApplication.Domain;
using Tp1_WebApplication.Utilities;
using Tp1_WebApplication.ViewModels;

namespace Tp1_WebApplication.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly DomainAsserts _asserts;

        public UserController(UserManager<User> userManager,
                          RoleManager<IdentityRole<Guid>> roleManager,
                          DomainAsserts asserts)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._asserts = asserts;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Manage(bool success = false, string actionType = "")
        {
            var vm = new List<UserDetailViewModel>();

            try
            {
                foreach (var user in _userManager.Users)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    vm.Add(new UserDetailViewModel
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        RoleName = userRoles.SingleOrDefault()
                    });
                }
            }
            catch (Exception ex)
            {
                ViewBag.UserListErrorMessage = "Error loading user list. Please try again." + ex.Message;
            }

            if (success)
            {
                if (actionType == "Remove")
                {
                    ViewBag.RemoveSuccessMessage = "The user was successfully removed.";
                }
                else if (actionType == "Create")
                {
                    ViewBag.CreateSuccessMessage = "The user was successfully created.";
                }
            }

            return View(vm);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            var passwordGenerated = PasswordGenerator.Generate();

            return View(new UserCreateViewModel()
            {
                Pwd = passwordGenerated,
                ConfirmPwd = passwordGenerated
            });
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var roles = await _roleManager.Roles.ToListAsync();
            var selectedRole = roles.FirstOrDefault(r => r.Id == vm.RoleId);

            if (selectedRole == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid role selected. Please try again.");
                return View(vm);
            }

            var existingUser = await _userManager.FindByNameAsync(vm.UserName);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Username already exists. Please choose a different username.");
                return View(vm);
            }

            var toCreate = new User(vm.UserName);
            var result = await _userManager.CreateAsync(toCreate, vm.Pwd);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "User creation failed. Please try again.");
                return View(vm);
            }

            result = await _userManager.AddToRoleAsync(toCreate, selectedRole.Name);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, $"Unable to add user to the role {selectedRole.Name}. Please try again.");
                return View(vm);
            }

            return RedirectToAction(nameof(Manage), new { success = true, actionType = "Create" });
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Remove(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            _asserts.Exists(user, "User not found. Please try again.");

            var result = await _userManager.DeleteAsync(user!);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Unable to remove user. Please try again or call emergency services if you are in danger.");
                return View();
            }

            return RedirectToAction(nameof(Manage), new { success = true, actionType = "Remove" });
        }
    }
}
