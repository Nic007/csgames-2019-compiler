using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {
        private readonly IAsyncRepository<Basket> _basketRepository; // @issue@I02
        private readonly IUriComposer _uriComposer; // @issue@I02
        private readonly IRepository<CatalogItem> _itemRepository; // @issue@I02

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
                var itemModel = new BasketItemViewModel()
                {
                    Id = i.Id,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    CatalogItemId = i.CatalogItemId

                };
                var item = _itemRepository.GetById(i.CatalogItemId);
                itemModel.PictureUrl = _uriComposer.ComposePicUri(item.PictureUri);
                itemModel.ProductName = item.Name;
                return itemModel;
            })
                            .ToList();
            return viewModel; // @issue@I02
        }

        private async Task<BasketViewModel> CreateBasketForUser(string userId) // @issue@I02
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
