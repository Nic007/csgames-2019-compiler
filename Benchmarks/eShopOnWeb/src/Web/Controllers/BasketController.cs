using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.eShopWeb.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.eShopWeb.Web.Interfaces;

namespace Microsoft.eShopWeb.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService; // @issue@I02
        private readonly IUriComposer _uriComposer; // @issue@I02 // @issue@I03
        private readonly SignInManager<ApplicationUser> _signInManager; // @issue@I02
        private readonly IAppLogger<BasketController> _logger; // @issue@I02 // @issue@I03
        private readonly IOrderService _orderService; // @issue@I02
        private readonly IBasketViewModelService _basketViewModelService; // @issue@I02

        public BasketController(IBasketService basketService, // @issue@I02
            IBasketViewModelService basketViewModelService,
            IOrderService orderService,
            IUriComposer uriComposer,
            SignInManager<ApplicationUser> signInManager,
            IAppLogger<BasketController> logger)
        {
            _basketService = basketService; // @issue@I02
            _uriComposer = uriComposer; // @issue@I02
            _signInManager = signInManager; // @issue@I02
            _logger = logger; // @issue@I02
            _orderService = orderService; // @issue@I02
            _basketViewModelService = basketViewModelService; // @issue@I02
        }

        [HttpGet]
        public async Task<IActionResult> Index() // @issue@I02
        {
            var basketModel = await GetBasketViewModelAsync(); // @issue@I02

            return View(basketModel); // @issue@I02
        }

        [HttpPost]
        public async Task<IActionResult> Index(Dictionary<string, int> items) // @issue@I02
        {
            var basketViewModel = await GetBasketViewModelAsync(); // @issue@I02
            await _basketService.SetQuantities(basketViewModel.Id, items); // @issue@I02

            return View(await GetBasketViewModelAsync()); // @issue@I02
        }


        // POST: /Basket/AddToBasket
        [HttpPost]
        public async Task<IActionResult> AddToBasket(CatalogItemViewModel productDetails) // @issue@I02
        {
            if (productDetails?.Id == null) // @issue@I02
            {
                return RedirectToAction("Index", "Catalog"); // @issue@I02
            }
            var basketViewModel = await GetBasketViewModelAsync(); // @issue@I02

            await _basketService.AddItemToBasket(basketViewModel.Id, productDetails.Id, productDetails.Price, 1); // @issue@I02

            return RedirectToAction("Index"); // @issue@I02
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Checkout(Dictionary<string, int> items) // @issue@I02
        {
            var basketViewModel = await GetBasketViewModelAsync(); // @issue@I02
            await _basketService.SetQuantities(basketViewModel.Id, items); // @issue@I02

            await _orderService.CreateOrderAsync(basketViewModel.Id, new Address("123 Main St.", "Kent", "OH", "United States", "44240")); // @issue@I02

            await _basketService.DeleteBasketAsync(basketViewModel.Id); // @issue@I02

            return View("Checkout"); // @issue@I02
        }

        private async Task<BasketViewModel> GetBasketViewModelAsync() // @issue@I02
        {
            if (_signInManager.IsSignedIn(HttpContext.User)) // @issue@I02
            {
                return await _basketViewModelService.GetOrCreateBasketForUser(User.Identity.Name); // @issue@I02
            }
            string anonymousId = GetOrSetBasketCookie(); // @issue@I02
            return await _basketViewModelService.GetOrCreateBasketForUser(anonymousId); // @issue@I02
        }

        private string GetOrSetBasketCookie() // @issue@I02
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME)) // @issue@I02
            {
                return Request.Cookies[Constants.BASKET_COOKIENAME]; // @issue@I02
            }
            string anonymousId = Guid.NewGuid().ToString(); // @issue@I02
            var cookieOptions = new CookieOptions(); // @issue@I02
            cookieOptions.Expires = DateTime.Today.AddYears(10); // @issue@I02
            Response.Cookies.Append(Constants.BASKET_COOKIENAME, anonymousId, cookieOptions); // @issue@I02
            return anonymousId; // @issue@I02
        }
    }
}
