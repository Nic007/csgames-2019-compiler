using Microsoft.eShopWeb.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Controllers.Api
{
    public class CatalogController : BaseApiController
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService) => _catalogService = catalogService; // @issue@I02

        [HttpGet]
        public async Task<IActionResult> List(int? brandFilterApplied, int? typesFilterApplied, int? page) // @issue@I02
        {
            var itemsPage = 10; // @issue@I02
            var catalogModel = await _catalogService.GetCatalogItems(page ?? 0, itemsPage, brandFilterApplied, typesFilterApplied); // @issue@I02
            return Ok(catalogModel); // @issue@I02
        }
    }
}
