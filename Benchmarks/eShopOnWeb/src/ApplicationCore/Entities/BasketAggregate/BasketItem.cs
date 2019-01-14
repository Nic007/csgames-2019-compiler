namespace Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate
{
    public class BasketItem : BaseEntity
    {
        public decimal UnitPrice { get; set; } // @trap@I01
        public int Quantity { get; set; }
        public int CatalogItemId { get; set; }
    }
}
