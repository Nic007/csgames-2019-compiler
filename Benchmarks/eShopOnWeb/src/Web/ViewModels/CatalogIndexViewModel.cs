using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class CatalogIndexViewModel
    {
        public IEnumerable<CatalogItemViewModel> CatalogItems { get; set; } // @issue@I02
        public IEnumerable<SelectListItem> Brands { get; set; } // @issue@I02
        public IEnumerable<SelectListItem> Types { get; set; } // @issue@I02
        public int? BrandFilterApplied { get; set; } // @issue@I02
        public int? TypesFilterApplied { get; set; } // @issue@I02
        public PaginationInfoViewModel PaginationInfo { get; set; } // @issue@I02
    }
}
