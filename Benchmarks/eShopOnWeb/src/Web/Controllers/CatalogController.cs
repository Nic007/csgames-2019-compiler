using Microsoft.eShopWeb.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Controllers
{
    [Route("")]
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService; // @issue@I02

        public CatalogController(ICatalogService catalogService) => _catalogService = catalogService; // @issue@I02

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index(int? brandFilterApplied, int? typesFilterApplied, int? page) // @issue@I02
        {
            var itemsPage = 10; // @issue@I02 // @issue@I03
            var catalogModel = await _catalogService.GetCatalogItems(page ?? 0, itemsPage, brandFilterApplied, typesFilterApplied); // @issue@I02
            return View(catalogModel); // @issue@I02
        }

        [HttpGet("Error")]
        public IActionResult Error() // @issue@I02
        {
            return View(); // @issue@I02
        }
    }
}
