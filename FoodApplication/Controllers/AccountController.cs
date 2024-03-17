using FoodApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace FoodApplication.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		public AccountController(UserManager<ApplicationUser> userManager, 
			SignInManager<ApplicationUser> signInManager)
		{
			this._userManager = userManager;
			this._signInManager = signInManager;
		}

		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel login)
		{
			if (ModelState.IsValid)
			{
				ApplicationUser user = new ApplicationUser() { Email = login.Email };
				var result = await this._signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Invalid LogIn Attempt");
				}

			}
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel register)
		{
			if (ModelState.IsValid)
			{
				ApplicationUser user = new ApplicationUser()
				{
					Name = register.Name,
					Address = register.Address,
					Email = register.Email,
					UserName = register.Email
				};

				IdentityResult result = await this._userManager.CreateAsync(user, register.Password);

				if (result.Succeeded)
				{
					await this._signInManager.PasswordSignInAsync(user, register.Password, false, false);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					foreach (IdentityError err in result.Errors)
					{
						ModelState.AddModelError("", err.Description);
					}
				}

			}
			return View(register);
		}
	}
}
