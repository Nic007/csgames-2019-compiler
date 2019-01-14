using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public class CustomerOrdersWithItemsSpecification : BaseSpecification<Order>
    {
        public CustomerOrdersWithItemsSpecification(string buyerId) : base(o => o.BuyerId == buyerId) // @issue@I02
        {
            AddInclude(o => o.OrderItems); // @issue@I02
            AddInclude($"{nameof(Order.OrderItems)}.{nameof(OrderItem.ItemOrdered)}"); // @issue@I02
        }
    }
}
