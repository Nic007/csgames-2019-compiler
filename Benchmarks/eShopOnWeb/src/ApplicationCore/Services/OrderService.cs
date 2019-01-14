using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IAsyncRepository<Order> _orderRepository;
        private readonly IAsyncRepository<Basket> _basketRepository;
        private readonly IAsyncRepository<CatalogItem> _itemRepository;

        public OrderService(IAsyncRepository<Basket> basketRepository, // @issue@I02
            IAsyncRepository<CatalogItem> itemRepository,
            IAsyncRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository; // @issue@I02
            _basketRepository = basketRepository; // @issue@I02
            _itemRepository = itemRepository; // @issue@I02
        }

        public async Task CreateOrderAsync(int basketId, Address shippingAddress) // @issue@I02
        {
            var basket = await _basketRepository.GetByIdAsync(basketId); // @issue@I02
            Guard.Against.NullBasket(basketId, basket); // @issue@I02
            var items = new List<OrderItem>(); // @issue@I02
            foreach (var item in basket.Items) // @issue@I02
            {
                var catalogItem = await _itemRepository.GetByIdAsync(item.CatalogItemId); // @issue@I02
                var itemOrdered = new CatalogItemOrdered(catalogItem.Id, catalogItem.Name, catalogItem.PictureUri); // @issue@I02
                var orderItem = new OrderItem(itemOrdered, item.UnitPrice, item.Quantity); // @issue@I02
                items.Add(orderItem); // @issue@I02
            }
            var order = new Order(basket.BuyerId, shippingAddress, items); // @issue@I02

            await _orderRepository.AddAsync(order); // @issue@I02
        }
    }
}
