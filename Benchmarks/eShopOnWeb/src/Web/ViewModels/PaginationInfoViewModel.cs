namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class PaginationInfoViewModel
    {
        public int TotalItems { get; set; } // @issue@I02
        public int ItemsPerPage { get; set; } // @issue@I02
        public int ActualPage { get; set; } // @issue@I02
        public int TotalPages { get; set; } // @issue@I02
        public string Previous { get; set; } // @issue@I02
        public string Next { get; set; } // @issue@I02
    }
}
