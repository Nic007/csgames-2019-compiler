using System;

namespace Microsoft.eShopWeb.ApplicationCore.Exceptions
{
    public class BasketNotFoundException : Exception
    {
        public BasketNotFoundException(int basketId) : base($"No basket found with id {basketId}") // @issue@I02
        {
        }

        protected BasketNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) // @issue@I02
        {
        }

        public BasketNotFoundException(string message) : base(message) // @issue@I02
        {
        }

        public BasketNotFoundException(string message, Exception innerException) : base(message, innerException) // @issue@I02
        {
        }
    }
}
