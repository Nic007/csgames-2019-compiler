using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.RazorPages.ViewComponents
{
    public class Basket : ViewComponent
    {
        private readonly IBasketService _basketService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public Basket(IBasketService basketService, // @issue@I02
                        SignInManager<ApplicationUser> signInManager)
        {
            _basketService = basketService; // @issue@I02
            _signInManager = signInManager; // @issue@I02
        }

        public async Task<IViewComponentResult> InvokeAsync() // @issue@I02
        {
            var vm = new BasketComponentViewModel(); // @issue@I02
            string userName = GetUsername(); // @issue@I02
            vm.ItemsCount = (await _basketService.GetBasketItemCountAsync(userName)); // @issue@I02
            return View(vm); // @issue@I02
        }

        public class BasketComponentViewModel
        {
            public int ItemsCount { get; set; } // @issue@I02
        }

        private string GetUsername() // @issue@I02
        {
            if (_signInManager.IsSignedIn(HttpContext.User)) // @issue@I02
            {
                return User.Identity.Name; // @issue@I02
            }
            return GetBasketIdFromCookie() ?? Constants.DEFAULT_USERNAME; // @issue@I02
        }

        private string GetBasketIdFromCookie() // @issue@I02
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME)) // @issue@I02
            {
                return Request.Cookies[Constants.BASKET_COOKIENAME]; // @issue@I02
            }
            return null; // @issue@I02
        }
    }
}
