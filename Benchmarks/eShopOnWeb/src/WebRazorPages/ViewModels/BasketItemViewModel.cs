namespace Microsoft.eShopWeb.RazorPages.ViewModels
{

    public class BasketItemViewModel
    {
        public int Id { get; set; } // @issue@I02
        public int CatalogItemId { get; set; } // @issue@I02
        public string ProductName { get; set; } // @issue@I02
        public decimal UnitPrice { get; set; } // @issue@I02
        public decimal OldUnitPrice { get; set; } // @issue@I02
        public int Quantity { get; set; } // @issue@I02
        public string PictureUrl { get; set; } // @issue@I02
    }
}
