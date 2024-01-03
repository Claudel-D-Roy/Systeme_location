using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tp1_CoreApplication.Domain;
using Tp1_WebApplication.Utilities;
using Tp1_WebApplication.ViewModels;

namespace Tp1_WebApplication.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AccountController(UserManager<User> userManager,
                             SignInManager<User> signInManager)
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
    }

    [AllowAnonymous]
    public IActionResult LogIn(string? returnUrl = "")
    {
        ViewBag.ReturnUrl = returnUrl;
        ViewBag.LogoutSuccessMessage = TempData["LogoutSuccessMessage"] as string;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> LogIn(LogInViewModel vm, string? returnUrl = "")
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(vm);
        }

        try
        {
            var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Pwd, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Login failed. Please try again.");
                ViewBag.ReturnUrl = returnUrl;
                return View(vm);
            }

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Manage", "Branch");
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "Login failed. Please try again.");
            ViewBag.ReturnUrl = returnUrl;
            return View(vm);
        }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        TempData["LogoutSuccessMessage"] = "You have been successfully logged out.";
        return RedirectToAction(nameof(LogIn));
    }

    [Authorize(Roles = "Administrator")]
    public IActionResult ResetPwd()
    {
        var viewModel = new ResetPwdViewModel();

        return View(viewModel);
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet]
    public async Task<IActionResult> ResetPwd(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user == null)
        {
            return NotFound();
        }

        var passwordGenerated = PasswordGenerator.Generate();


        var viewModel = new ResetPwdViewModel
        {
            UserId = user.Id,
            UserName = user.UserName,
            NewPassword = passwordGenerated,
            ConfirmNewPassword = passwordGenerated
        };

        return View(viewModel);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public async Task<IActionResult> ResetPwd(ResetPwdViewModel viewModel)
    {
        ModelState.Remove("UserName");
        if (!ModelState.IsValid)
        {
            var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
            return View(viewModel);
        }

        var user = await _userManager.FindByIdAsync(viewModel.UserId.ToString());
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, viewModel.NewPassword);

        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Unable to reset password. Please try again.");
            return View(viewModel);
        }

        TempData["ResetSuccessMessage"] = "Password reset succeeded.";

        return RedirectToAction("Manage", "User");
    }
}
