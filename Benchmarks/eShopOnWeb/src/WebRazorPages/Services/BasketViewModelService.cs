using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Linq;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.RazorPages.Interfaces;
using Microsoft.eShopWeb.RazorPages.ViewModels;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;

namespace Microsoft.eShopWeb.RazorPages.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {
        private readonly IAsyncRepository<Basket> _basketRepository;
        private readonly IUriComposer _uriComposer;
        private readonly IRepository<CatalogItem> _itemRepository;

        public BasketViewModelService(IAsyncRepository<Basket> basketRepository, // @issue@I02
            IRepository<CatalogItem> itemRepository,
            IUriComposer uriComposer)
        {
            _basketRepository = basketRepository; // @issue@I02
            _uriComposer = uriComposer; // @issue@I02
            _itemRepository = itemRepository; // @issue@I02
        }

        public async Task<BasketViewModel> GetOrCreateBasketForUser(string userName) // @issue@I02
        {
            var basketSpec = new BasketWithItemsSpecification(userName); // @issue@I02
            var basket = (await _basketRepository.ListAsync(basketSpec)).FirstOrDefault(); // @issue@I02

            if (basket == null) // @issue@I02
            {
                return await CreateBasketForUser(userName); // @issue@I02
            }
            return CreateViewModelFromBasket(basket); // @issue@I02
        }

        private BasketViewModel CreateViewModelFromBasket(Basket basket) // @issue@I02
        {
            var viewModel = new BasketViewModel(); // @issue@I02
            viewModel.Id = basket.Id; // @issue@I02
            viewModel.BuyerId = basket.BuyerId; // @issue@I02
            viewModel.Items = basket.Items.Select(i => // @issue@I02
            {
                var itemModel = new BasketItemViewModel() // @issue@I02
                {
                    Id = i.Id,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    CatalogItemId = i.CatalogItemId

                };
                var item = _itemRepository.GetById(i.CatalogItemId); // @issue@I02
                itemModel.PictureUrl = _uriComposer.ComposePicUri(item.PictureUri); // @issue@I02
                itemModel.ProductName = item.Name; // @issue@I02
                return itemModel; // @issue@I02
            })
                            .ToList(); // @issue@I02
            return viewModel; // @issue@I02
        }

        private async Task<BasketViewModel> CreateBasketForUser(string userId)
        {
            var basket = new Basket() { BuyerId = userId }; // @issue@I02
            await _basketRepository.AddAsync(basket); // @issue@I02

            return new BasketViewModel() // @issue@I02
            {
                BuyerId = basket.BuyerId,
                Id = basket.Id,
                Items = new List<BasketItemViewModel>()
            };
        }
    }
}
