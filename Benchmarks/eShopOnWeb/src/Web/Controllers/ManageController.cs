using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.Web.ViewModels.Manage;
using Microsoft.eShopWeb.Web.Services;
using System;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<ManageController> _logger;
        private readonly UrlEncoder _urlEncoder;

        private const string AuthenicatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public ManageController( // @issue@I02
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          IEmailSender emailSender,
          IAppLogger<ManageController> logger,
          UrlEncoder urlEncoder)
        {
            _userManager = userManager; // @issue@I02
            _signInManager = signInManager; // @issue@I02
            _emailSender = emailSender; // @issue@I02
            _logger = logger; // @issue@I02
            _urlEncoder = urlEncoder; // @issue@I02
        }

        [TempData]
        public string StatusMessage { get; set; } // @issue@I02

        [HttpGet]
        public async Task<IActionResult> Index() // @issue@I02
        {
            var user = await _userManager.GetUserAsync(User); // @issue@I02
            if (user == null) // @issue@I02
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'."); // @issue@I02
            }

            var model = new IndexViewModel // @issue@I02
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage
            };

            return View(model); // @issue@I02
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model) // @issue@I02 // @issue@I05
        {
            if (!ModelState.IsValid) // @issue@I02
            {
                return View(model); // @issue@I02
            }

            var user = await _userManager.GetUserAsync(User); // @issue@I02
            if (user == null) // @issue@I02
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'."); // @issue@I02
            }

            var email = user.Email; // @issue@I02
            if (model.Email != email) // @issue@I02
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email); // @issue@I02
                if (!setEmailResult.Succeeded) // @issue@I02
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'."); // @issue@I02
                }
            }

            var phoneNumber = user.PhoneNumber; // @issue@I02
            if (model.PhoneNumber != phoneNumber) // @issue@I02
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber); // @issue@I02
                if (!setPhoneResult.Succeeded) // @issue@I02
                {
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'."); // @issue@I02
                }
            }

            StatusMessage = "Your profile has been updated"; // @issue@I02
            return RedirectToAction(nameof(Index)); // @issue@I02
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendVerificationEmail(IndexViewModel model) // @issue@I02
        {
            if (!ModelState.IsValid) // @issue@I02
            {
                return View(model); // @issue@I02
            }

            var user = await _userManager.GetUserAsync(User); // @issue@I02
            if (user == null) // @issue@I02
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'."); // @issue@I02
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user); // @issue@I02
            var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme); // @issue@I02
            var email = user.Email; // @issue@I02
            await _emailSender.SendEmailConfirmationAsync(email, callbackUrl); // @issue@I02

            StatusMessage = "Verification email sent. Please check your email."; // @issue@I02
            return RedirectToAction(nameof(Index)); // @issue@I02
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword() // @issue@I02
        {
            var user = await _userManager.GetUserAsync(User); // @issue@I02
            if (user == null) // @issue@I02
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'."); // @issue@I02
            }

            var hasPassword = await _userManager.HasPasswordAsync(user); // @issue@I02
            if (!hasPassword) // @issue@I02
            {
                return RedirectToAction(nameof(SetPassword)); // @issue@I02
            }

            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage }; // @issue@I02
            return View(model); // @issue@I02
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model) // @issue@I02 // @trap@I05
        {
            if (!ModelState.IsValid) // @issue@I02
            {
                return View(model); // @issue@I02
            }

            var user = await _userManager.GetUserAsync(User); // @issue@I02
            if (user == null) // @issue@I02
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'."); // @issue@I02
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword); // @issue@I02
            if (!changePasswordResult.Succeeded) // @issue@I02
            {
                AddErrors(changePasswordResult); // @issue@I02
                return View(model); // @issue@I02
            }

            await _signInManager.SignInAsync(user, isPersistent: false); // @issue@I02
            _logger.LogInformation("User changed their password successfully."); // @issue@I02
            StatusMessage = "Your password has been changed."; // @issue@I02

            return RedirectToAction(nameof(ChangePassword)); // @issue@I02
        }

        [HttpGet]
        public async Task<IActionResult> SetPassword() // @issue@I02
        {
            var user = await _userManager.GetUserAsync(User); // @issue@I02
            if (user == null) // @issue@I02
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'."); // @issue@I02
            }

            var hasPassword = await _userManager.HasPasswordAsync(user); // @issue@I02

            if (hasPassword) // @issue@I02
            {
                return RedirectToAction(nameof(ChangePassword)); // @issue@I02
            }

            var model = new SetPasswordViewModel { StatusMessage = StatusMessage }; // @issue@I02
            return View(model); // @issue@I02
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model) // @issue@I02
        {
            if (!ModelState.IsValid) // @issue@I02
            {
                return View(model); // @issue@I02
            }

            var user = await _userManager.GetUserAsync(User); // @issue@I02
            if (user == null) // @issue@I02
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'."); // @issue@I02
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword); // @issue@I02
            if (!addPasswordResult.Succeeded) // @issue@I02
            {
                AddErrors(addPasswordResult); // @issue@I02
                return View(model); // @issue@I02
            }

            await _signInManager.SignInAsync(user, isPersistent: false); // @issue@I02
            StatusMessage = "Your password has been set."; // @issue@I02

            return RedirectToAction(nameof(SetPassword)); // @issue@I02
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLogins() // @issue@I02
        {
            var user = await _userManager.GetUserAsync(User); // @issue@I02
            if (user == null) // @issue@I02
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'."); // @issue@I02
            }

            var model = new ExternalLoginsViewModel { CurrentLogins = await _userManager.GetLoginsAsync(user) }; // @issue@I02
            model.OtherLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()) // @issue@I02
                .Where(auth => model.CurrentLogins.All(ul => auth.Name != ul.LoginProvider))
                .ToList();
            model.ShowRemoveButton = await _userManager.HasPasswordAsync(user) || model.CurrentLogins.Count > 1; // @issue@I02
            model.StatusMessage = StatusMessage; // @issue@I02

            return View(model); // @issue@I02
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LinkLogin(string provider) // @issue@I02
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme); // @issue@I02

            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Action(nameof(LinkLoginCallback)); // @issue@I02
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User)); // @issue@I02
            return new ChallengeResult(provider, properties); // @issue@I02
        }

        [HttpGet]
        public async Task<IActionResult> LinkLoginCallback() // @issue@I02
        {
            var user = await _userManager.GetUserAsync(User); // @issue@I02
            if (user == null) // @issue@I02
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'."); // @issue@I02
            }

            var info = await _signInManager.GetExternalLoginInfoAsync(user.Id); // @issue@I02
            if (info == null) // @issue@I02
            {
                throw new ApplicationException($"Unexpected error occurred loading external login info for user with ID '{user.Id}'."); // @issue@I02
            }

            var result = await _userManager.AddLoginAsync(user, info); // @issue@I02
            if (!result.Succeeded) // @issue@I02
            {
                throw new ApplicationException($"Unexpected error occurred adding external login for user with ID '{user.Id}'."); // @issue@I02
            }

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme); // @issue@I02

            StatusMessage = "The external login was added."; // @issue@I02
            return RedirectToAction(nameof(ExternalLogins)); // @issue@I02
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLogin(RemoveLoginViewModel model) // @issue@I02
        {
            var user = await _userManager.GetUserAsync(User); // @issue@I02
            if (user == null) // @issue@I02
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'."); // @issue@I02
            }

            var result = await _userManager.RemoveLoginAsync(user, model.LoginProvider, model.ProviderKey); // @issue@I02
            if (!result.Succeeded) // @issue@I02
            {
                throw new ApplicationException($"Unexpected error occurred removing external login for user with ID '{user.Id}'."); // @issue@I02
            }

            await _signInManager.SignInAsync(user, isPersistent: false); // @issue@I02
            StatusMessage = "The external login was removed."; // @issue@I02
            return RedirectToAction(nameof(ExternalLogins)); // @issue@I02
        }

        [HttpGet]
        public async Task<IActionResult> TwoFactorAuthentication() // @issue@I02
        {
            var user = await _userManager.GetUserAsync(User); // @issue@I02
            if (user == null) // @issue@I02
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'."); // @issue@I02
            }

            var model = new TwoFactorAuthenticationViewModel // @issue@I02
            {
                HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null,
                Is2faEnabled = user.TwoFactorEnabled,
                RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user),
            };

            return View(model); // @issue@I02
        }

        [HttpGet]
        public async Task<IActionResult> Disable2faWarning() // @issue@I02
        {
            var user = await _userManager.GetUserAsync(User); // @issue@I02
            if (user == null) // @issue@I02
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'."); // @issue@I02
            }

            if (!user.TwoFactorEnabled) // @issue@I02
            {
                throw new ApplicationException($"Unexpected error occured disabling 2FA for user with ID '{user.Id}'."); // @issue@I02
            }

            return View(nameof(Disable2fa)); // @issue@I02
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disable2fa() // @issue@I02
        {
            var user = await _userManager.GetUserAsync(User); // @issue@I02
            if (user == null) // @issue@I02
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'."); // @issue@I02
            }

            var disable2faResult = await _userManager.SetTwoFactorEnabledAsync(user, false); // @issue@I02
            if (!disable2faResult.Succeeded) // @issue@I02
            {
                throw new ApplicationException($"Unexpected error occured disabling 2FA for user with ID '{user.Id}'."); // @issue@I02
            }

            _logger.LogInformation("User with ID {UserId} has disabled 2fa.", user.Id); // @issue@I02
            return RedirectToAction(nameof(TwoFactorAuthentication)); // @issue@I02
        }

        [HttpGet]
        public async Task<IActionResult> EnableAuthenticator() // @issue@I02
        {
            var user = await _userManager.GetUserAsync(User); // @issue@I02
            if (user == null) // @issue@I02
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'."); // @issue@I02
            }

            var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user); // @issue@I02
            if (string.IsNullOrEmpty(unformattedKey)) // @issue@I02
            {
                await _userManager.ResetAuthenticatorKeyAsync(user); // @issue@I02
                unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user); // @issue@I02
            }

            var model = new EnableAuthenticatorViewModel // @issue@I02
            {
                SharedKey = FormatKey(unformattedKey), // @issue@I02
                AuthenticatorUri = GenerateQrCodeUri(user.Email, unformattedKey) // @issue@I02
            };

            return View(model); // @issue@I02
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableAuthenticator(EnableAuthenticatorViewModel model) // @issue@I02 // @trap@I05
        {
            if (!ModelState.IsValid) // @issue@I02
            {
                return View(model); // @issue@I02
            }

            var user = await _userManager.GetUserAsync(User); // @issue@I02
            if (user == null) // @issue@I02
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'."); // @issue@I02
            }

            // Strip spaces and hypens
            var verificationCode = model.Code.Replace(" ", string.Empty).Replace("-", string.Empty); // @issue@I02

            var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync( // @issue@I02
                user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode); // @issue@I02

            if (!is2faTokenValid) // @issue@I02
            {
                ModelState.AddModelError("model.TwoFactorCode", "Verification code is invalid."); // @issue@I02
                return View(model); // @issue@I02
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true); // @issue@I02
            _logger.LogInformation("User with ID {UserId} has enabled 2FA with an authenticator app.", user.Id); // @issue@I02
            return RedirectToAction(nameof(GenerateRecoveryCodes)); // @issue@I02
        }

        [HttpGet]
        public IActionResult ResetAuthenticatorWarning() // @issue@I02 // @issue@I04
        {
            return View(nameof(ResetAuthenticator)); // @issue@I02
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetAuthenticator() // @issue@I02
        {
            var user = await _userManager.GetUserAsync(User); // @issue@I02
            if (user == null) // @issue@I02
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'."); // @issue@I02
            }

            await _userManager.SetTwoFactorEnabledAsync(user, false); // @issue@I02
            await _userManager.ResetAuthenticatorKeyAsync(user); // @issue@I02
            _logger.LogInformation("User with id '{UserId}' has reset their authentication app key.", user.Id); // @issue@I02

            return RedirectToAction(nameof(EnableAuthenticator)); // @issue@I02
        }

        [HttpGet]
        public async Task<IActionResult> GenerateRecoveryCodes() // @issue@I02
        {
            var user = await _userManager.GetUserAsync(User); // @issue@I02
            if (user == null) // @issue@I02
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'."); // @issue@I02
            }

            if (!user.TwoFactorEnabled) // @issue@I02
            {
                throw new ApplicationException($"Cannot generate recovery codes for user with ID '{user.Id}' as they do not have 2FA enabled."); // @issue@I02
            }

            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10); // @issue@I02
            var model = new GenerateRecoveryCodesViewModel { RecoveryCodes = recoveryCodes.ToArray() }; // @issue@I02

            _logger.LogInformation("User with ID {UserId} has generated new 2FA recovery codes.", user.Id); // @issue@I02

            return View(model); // @issue@I02
        }

        private void AddErrors(IdentityResult result) // @issue@I02 // @issue@I04
        {
            foreach (var error in result.Errors) // @issue@I02
            {
                ModelState.AddModelError(string.Empty, error.Description); // @issue@I02
            }
        }

        private string FormatKey(string unformattedKey) // @issue@I02
        {
            var result = new StringBuilder(); // @issue@I02
            int currentPosition = 0; // @issue@I02
            while (currentPosition + 4 < unformattedKey.Length) // @issue@I02
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" "); // @issue@I02
                currentPosition += 4; // @issue@I02
            }
            if (currentPosition < unformattedKey.Length) // @issue@I02
            {
                result.Append(unformattedKey.Substring(currentPosition)); // @issue@I02
            }

            return result.ToString().ToLowerInvariant(); // @issue@I02
        }

        private string GenerateQrCodeUri(string email, string unformattedKey) // @issue@I02
        {
            return string.Format( // @issue@I02
                AuthenicatorUriFormat,
                _urlEncoder.Encode("eShopOnWeb"),
                _urlEncoder.Encode(email),
                unformattedKey);
        }
    }
}
