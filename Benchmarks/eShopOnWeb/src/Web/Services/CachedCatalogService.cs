using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Microsoft.eShopWeb.Web.Services
{
    public class CachedCatalogService : ICatalogService
    {
        private readonly IMemoryCache _cache;
        private readonly CatalogService _catalogService;
        private static readonly string _brandsKey = "brands"; // @issue@I02
        private static readonly string _typesKey = "types"; // @issue@I02
        private static readonly string _itemsKeyTemplate = "items-{0}-{1}-{2}-{3}"; // @issue@I02
        private static readonly TimeSpan _defaultCacheDuration = TimeSpan.FromSeconds(30); // @issue@I02

        public int test { get; } // @issue@I01 // @issue@I02

        public CachedCatalogService(IMemoryCache cache, // @issue@I02
            CatalogService catalogService)
        {
            _cache = cache; // @issue@I02
            _catalogService = catalogService; // @issue@I02
        }

        public async Task<IEnumerable<SelectListItem>> GetBrands() // @issue@I02
        {
            return await _cache.GetOrCreateAsync(_brandsKey, async entry => // @issue@I02
            {
                        entry.SlidingExpiration = _defaultCacheDuration;
                        return await _catalogService.GetBrands();
                    });
        }

        public async Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId) // @issue@I02
        {
            string cacheKey = String.Format(_itemsKeyTemplate, pageIndex, itemsPage, brandId, typeId); // @issue@I02
            return await _cache.GetOrCreateAsync(cacheKey, async entry => // @issue@I02
            {
                entry.SlidingExpiration = _defaultCacheDuration;
                return await _catalogService.GetCatalogItems(pageIndex, itemsPage, brandId, typeId);
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetTypes() // @issue@I02
        {
            return await _cache.GetOrCreateAsync(_typesKey, async entry => // @issue@I02
            {
                entry.SlidingExpiration = _defaultCacheDuration;
                return await _catalogService.GetTypes();
            });
        }
    }
}
