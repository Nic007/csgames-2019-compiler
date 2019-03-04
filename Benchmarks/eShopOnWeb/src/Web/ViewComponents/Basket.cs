using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.ViewComponents
{
    public class Basket : ViewComponent
    {
        public string PropertyThatShouldBeStatic => "What Am I doing"; // @issue@I02 // @issue@I04

        private readonly IBasketViewModelService _basketService; // @issue@I02
        private readonly SignInManager<ApplicationUser> _signInManager; // @issue@I02

        public Basket(IBasketViewModelService basketService, // @issue@I02
                        SignInManager<ApplicationUser> signInManager)
        {
            _basketService = basketService; // @issue@I02
            _signInManager = signInManager; // @issue@I02
        }

        public async Task<IViewComponentResult> InvokeAsync(string userName) // @issue@I02
        {
            var vm = new BasketComponentViewModel(); // @issue@I02
            vm.ItemsCount = (await GetBasketViewModelAsync()).Items.Sum(i => i.Quantity); // @issue@I02
            return View(vm); // @issue@I02
        }

        private async Task<BasketViewModel> GetBasketViewModelAsync() // @issue@I02
        {
            if (_signInManager.IsSignedIn(HttpContext.User)) // @issue@I02
            {
                return await _basketService.GetOrCreateBasketForUser(User.Identity.Name); // @issue@I02
            }
            string anonymousId = GetBasketIdFromCookie(); // @issue@I02
            if (anonymousId == null) return new BasketViewModel(); // @issue@I02
            return await _basketService.GetOrCreateBasketForUser(anonymousId); // @issue@I02
        }

        private string GetBasketIdFromCookie() // @issue@I02 // @issue@I04
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME)) // @issue@I02
            {
                return Request.Cookies[Constants.BASKET_COOKIENAME]; // @issue@I02
            }
            return null; // @issue@I02
        }
    }
}
