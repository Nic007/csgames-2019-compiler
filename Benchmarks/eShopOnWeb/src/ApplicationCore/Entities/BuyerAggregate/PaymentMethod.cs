using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.BuyerAggregate
{
    public class PaymentMethod : BaseEntity
    {
        public string Alias { get; set; } // @issue@I02
        public string CardId { get; set; } // actual card data must be stored in a PCI compliant system, like Stripe // @issue@I02
        public string Last4 { get; set; } // @issue@I02
    }
}
