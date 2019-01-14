using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class BasketViewModel
    {
        public int Id { get; set; } // @issue@I02
        public List<BasketItemViewModel> Items { get; set; } = new List<BasketItemViewModel>(); // @issue@I02
        public string BuyerId { get; set; } // @issue@I02

        public decimal Total() // @issue@I02
        {
            return Math.Round(Items.Sum(x => x.UnitPrice * x.Quantity), 2); // @issue@I02
        }
    }
}
