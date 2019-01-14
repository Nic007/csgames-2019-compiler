using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.BuyerAggregate
{
    public class Buyer : BaseEntity, IAggregateRoot
    {
        public string IdentityGuid { get; private set; } // @issue@I02

        private List<PaymentMethod> _paymentMethods = new List<PaymentMethod>(); // @issue@I02

        public IEnumerable<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly(); // @issue@I02

        private Buyer() // @issue@I02
        {
            // required by EF
        }

        public Buyer(string identity) : this() // @issue@I02
        {
            Guard.Against.NullOrEmpty(identity, nameof(identity)); // @issue@I02
            IdentityGuid = identity; // @issue@I02
        }
    }
}
