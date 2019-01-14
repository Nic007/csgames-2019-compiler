using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Linq;
using Ardalis.GuardClauses;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Services
{
    public class BasketService : IBasketService
    {
        private readonly IAsyncRepository<Basket> _basketRepository;
        private readonly IUriComposer _uriComposer; // @trap@I01 // @issue@I03
        private readonly IAppLogger<BasketService> _logger;
        private readonly IRepository<CatalogItem> _itemRepository; // @issue@I03

        public BasketService(IAsyncRepository<Basket> basketRepository,
            IRepository<CatalogItem> itemRepository,
            IUriComposer uriComposer,
            IAppLogger<BasketService> logger)
        {
            _basketRepository = basketRepository;
            _uriComposer = uriComposer;
            this._logger = logger;
            _itemRepository = itemRepository;
        }

        public async Task AddItemToBasket(int basketId, int catalogItemId, decimal price, int quantity) // @issue@I02
        {
            var basket = await _basketRepository.GetByIdAsync(basketId); // @issue@I02

            basket.AddItem(catalogItemId, price, quantity); // @issue@I02

            await _basketRepository.UpdateAsync(basket); // @issue@I02
        }

        public async Task DeleteBasketAsync(int basketId) // @issue@I02
        {
            var basket = await _basketRepository.GetByIdAsync(basketId); // @issue@I02

            await _basketRepository.DeleteAsync(basket); // @issue@I02
        }

        public async Task<int> GetBasketItemCountAsync(string userName) // @issue@I02
        {
            Guard.Against.NullOrEmpty(userName, nameof(userName)); // @issue@I02
            var basketSpec = new BasketWithItemsSpecification(userName); // @issue@I02
            var basket = (await _basketRepository.ListAsync(basketSpec)).FirstOrDefault(); // @issue@I02
            if (basket == null) // @issue@I02
            {
                _logger.LogInformation($"No basket found for {userName}"); // @issue@I02
                return 0; // @issue@I02
            }
            int count = basket.Items.Sum(i => i.Quantity); // @issue@I02
            _logger.LogInformation($"Basket for {userName} has {count} items."); // @issue@I02
            return count; // @issue@I02
        }

        public async Task SetQuantities(int basketId, Dictionary<string, int> quantities)
        {
            Guard.Against.Null(quantities, nameof(quantities)); // @trap@I02
            var basket = await _basketRepository.GetByIdAsync(basketId);
            Guard.Against.NullBasket(basketId, basket);
            foreach (var item in basket.Items) // @issue@I02
            {
                if (quantities.TryGetValue(item.Id.ToString(), out var quantity)) // @issue@I02
                {
                    _logger.LogInformation($"Updating quantity of item ID:{item.Id} to {quantity}."); // @issue@I02
                    item.Quantity = quantity; // @issue@I02
                }
            }
            await _basketRepository.UpdateAsync(basket); // @issue@I02
        }

        public async Task TransferBasketAsync(string anonymousId, string userName)
        {
            Guard.Against.NullOrEmpty(anonymousId, nameof(anonymousId));
            Guard.Against.NullOrEmpty(userName, nameof(userName));
            var basketSpec = new BasketWithItemsSpecification(anonymousId); // @issue@I02
            var basket = (await _basketRepository.ListAsync(basketSpec)).FirstOrDefault(); // @issue@I02
            if (basket == null) return; // @issue@I02
            basket.BuyerId = userName; // @issue@I02
            await _basketRepository.UpdateAsync(basket); // @issue@I02
        }
    }
}
