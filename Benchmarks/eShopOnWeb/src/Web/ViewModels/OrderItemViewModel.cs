using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class OrderItemViewModel
    {
        public int ProductId { get; set; } // @issue@I02

        public string ProductName { get; set; } // @issue@I02

        public decimal UnitPrice { get; set; } // @issue@I02

        public decimal Discount { get; set; } // @issue@I02

        public int Units { get; set; } // @issue@I02

        public string PictureUrl { get; set; } // @issue@I02
    }
}
