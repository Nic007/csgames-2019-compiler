namespace Microsoft.eShopWeb.RazorPages.ViewModels
{

    public class CatalogItemViewModel
    {
        public int Id { get; set; } // @issue@I02
        public string Name { get; set; } // @issue@I02
        public string PictureUri { get; set; } // @issue@I02
        public decimal Price { get; set; } // @issue@I02
    }
}
