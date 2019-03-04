using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.Web.ViewModels.Account;
using System;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager; // @issue@I02
        private readonly SignInManager<ApplicationUser> _signInManager; // @issue@I02
        private readonly IBasketService _basketService; // @issue@I02
        private readonly IAppLogger<AccountController> _logger; // @issue@I02

        public AccountController( // @issue@I02
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IBasketService basketService,
            IAppLogger<AccountController> logger)
        {
            _userManager = userManager; // @issue@I02
            _signInManager = signInManager; // @issue@I02
            _basketService = basketService; // @issue@I02
            _logger = logger; // @issue@I02
        }

        // GET: /Account/SignIn 
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(string returnUrl = null) // @issue@I02 // @issue@I04
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme); // @issue@I02

            ViewData["ReturnUrl"] = returnUrl; // @issue@I02
            if (!String.IsNullOrEmpty(returnUrl) && // @issue@I02
                returnUrl.IndexOf("checkout", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                ViewData["ReturnUrl"] = "/Basket/Index"; // @issue@I02
            }

            return View(); // @issue@I02
        }

        // POST: /Account/SignIn
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginViewModel model, string returnUrl = null) // @issue@I02 // @trap@I05
        {
            if (!ModelState.IsValid) // @issue@I02
            {
                return View(model); // @issue@I02
            }
            ViewData["ReturnUrl"] = returnUrl; // @issue@I02

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false); // @issue@I02
            if (result.RequiresTwoFactor) // @issue@I02
            {
                return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe }); // @issue@I02
            }
            if (result.Succeeded) // @issue@I02
            {
                string anonymousBasketId = Request.Cookies[Constants.BASKET_COOKIENAME]; // @issue@I02
                if (!String.IsNullOrEmpty(anonymousBasketId)) // @issue@I02
                {
                    await _basketService.TransferBasketAsync(anonymousBasketId, model.Email); // @issue@I02
                    Response.Cookies.Delete(Constants.BASKET_COOKIENAME); // @issue@I02
                }
                return RedirectToLocal(returnUrl); // @issue@I02
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt."); // @issue@I02
            return View(model); // @issue@I02
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null) // @issue@I02
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync(); // @issue@I02

            if (user == null) // @issue@I02
            {
                throw new ApplicationException($"Unable to load two-factor authentication user."); // @issue@I02
            }

            var model = new LoginWith2faViewModel { RememberMe = rememberMe }; // @issue@I02
            ViewData["ReturnUrl"] = returnUrl; // @issue@I02

            return View(model); // @issue@I02
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, bool rememberMe, string returnUrl = null) // @issue@I02 // @issue@I05
        {
            if (!ModelState.IsValid) // @issue@I02
            {
                return View(model); // @issue@I02
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync(); // @issue@I02
            if (user == null) // @issue@I02
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'."); // @issue@I02
            }

            var authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty); // @issue@I02

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine); // @issue@I02

            if (result.Succeeded) // @issue@I02
            {
                _logger.LogInformation("User with ID {UserId} logged in with 2fa.", user.Id); // @issue@I02
                return RedirectToLocal(returnUrl); // @issue@I02
            }
            else if (result.IsLockedOut) // @issue@I02
            {
                _logger.LogWarning("User with ID {UserId} account locked out.", user.Id); // @issue@I02
                return RedirectToAction(nameof(Lockout)); // @issue@I02
            }
            else
            {
                _logger.LogWarning("Invalid authenticator code entered for user with ID {UserId}.", user.Id); // @issue@I02
                ModelState.AddModelError(string.Empty, "Invalid authenticator code."); // @issue@I02
                return View(); // @issue@I02
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout() // @issue@I02
        {
            return View(); // @issue@I02
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignOut() // @issue@I02
        {
            await _signInManager.SignOutAsync(); // @issue@I02

            return RedirectToAction(nameof(CatalogController.Index), "Catalog"); // @issue@I02
        }

        [AllowAnonymous]
        public IActionResult Register() // @issue@I02 // @issue@I04
        {
            return View(); // @issue@I02
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null) // @issue@I02
        {
            if (ModelState.IsValid) // @issue@I02
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email }; // @issue@I02
                var result = await _userManager.CreateAsync(user, model.Password); // @issue@I02
                if (result.Succeeded) // @issue@I02
                {
                    await _signInManager.SignInAsync(user, isPersistent: false); // @issue@I02
                    return RedirectToLocal(returnUrl); // @issue@I02
                }
                AddErrors(result); // @issue@I02
            }
            // If we got this far, something failed, redisplay form
            return View(model); // @issue@I02
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code) // @issue@I02
        {
            if (userId == null || code == null) // @issue@I02
            {
                return RedirectToAction(nameof(CatalogController.Index), "Catalog"); // @issue@I02
            }
            var user = await _userManager.FindByIdAsync(userId); // @issue@I02
            if (user == null) // @issue@I02
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'."); // @issue@I02
            }
            var result = await _userManager.ConfirmEmailAsync(user, code); // @issue@I02
            return View(result.Succeeded ? "ConfirmEmail" : "Error"); // @issue@I02
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null) // @issue@I02 // @issue@I04
        {
            if (code == null) // @issue@I02
            {
                throw new ApplicationException("A code must be supplied for password reset."); // @issue@I02
            }
            var model = new ResetPasswordViewModel { Code = code }; // @issue@I02
            return View(model); // @issue@I02
        }

        private IActionResult RedirectToLocal(string returnUrl) // @issue@I02 // @issue@I04
        {
            if (Url.IsLocalUrl(returnUrl)) // @issue@I02
            {
                return Redirect(returnUrl); // @issue@I02
            }
            else
            {
                return RedirectToAction(nameof(CatalogController.Index), "Catalog"); // @issue@I02
            }
        }

        private void AddErrors(IdentityResult result) // @issue@I02
        {
            foreach (var error in result.Errors) // @issue@I02
            {
                ModelState.AddModelError("", error.Description); // @issue@I02
            }
        }
    }
}
