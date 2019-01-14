using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.RazorPages.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAppLogger<AccountController> _logger;

        public AccountController(SignInManager<ApplicationUser> signInManager,  // @issue@I02
            IAppLogger<AccountController> logger)
        {
            _signInManager = signInManager; // @issue@I02
            _logger = logger; // @issue@I02
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout() // @issue@I02
        {
            await _signInManager.SignOutAsync(); // @issue@I02
            _logger.LogInformation("User logged out."); // @issue@I02
            return RedirectToPage("/Index"); // @issue@I02
        }
    }
}
