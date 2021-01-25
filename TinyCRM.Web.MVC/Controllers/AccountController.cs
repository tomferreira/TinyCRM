using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using TinyCRM.Application.ViewModels.Account;
using TinyCRM.Web.MVC.Resources;

namespace TinyCRM.Web.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;
        private readonly ISharedViewLocalizer _sharedLocalizer;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, 
            RoleManager<IdentityRole> roleManager,
            IOptions<RequestLocalizationOptions> localizationOptions,
            ISharedViewLocalizer sharedLocalizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _localizationOptions = localizationOptions;
            _sharedLocalizer = sharedLocalizer;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var data = await _userManager.FindByNameAsync(model.UserName);

            if (data == null)
            {
                ModelState.AddModelError(string.Empty, _sharedLocalizer["UsernameOrPasswordWrongMessage"]);
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(data, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, _sharedLocalizer["UsernameOrPasswordWrongMessage"]);
                return View(model);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Redirect(nameof(Login));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.PasswordConfirmation)
                {
                    ModelState.AddModelError("PasswordConfirmation", _sharedLocalizer["PasswdConfirmationDifferentMessage"]);
                    return View(model);
                }

                IdentityUser user = new IdentityUser { UserName = model.UserName, Email = model.Email };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var cultureFeature = HttpContext.Features.Get<IRequestCultureFeature>();

            var model = new ProfileViewModel();
            model.SupportedCultures = _localizationOptions.Value.SupportedUICultures.ToList();
            model.CurrentUICulture = cultureFeature.RequestCulture.UICulture;

            return View(model);
        }

        [HttpPost]
        public IActionResult Profile([FromForm] ProfileViewModel model)
        {
            // TODO: Persist the culture info

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(model.CurrentUICulture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return RedirectToAction(nameof(AccountController.Profile));
        }
    }
}
