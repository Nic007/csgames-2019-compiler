using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using System;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class OrderViewModel
    {
        public int OrderNumber { get; set; } // @issue@I02
        public DateTimeOffset OrderDate { get; set; } // @issue@I02
        public decimal Total { get; set; } // @issue@I02
        public string Status { get; set; } // @issue@I02

        public Address ShippingAddress { get; set; }  // @issue@I02

        public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>(); // @issue@I02

    }

}
