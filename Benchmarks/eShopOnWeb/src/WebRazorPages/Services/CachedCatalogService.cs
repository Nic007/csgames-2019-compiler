using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.RazorPages.Interfaces;
using Microsoft.eShopWeb.RazorPages.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.RazorPages.Services
{
    public class CachedCatalogService : ICatalogService
    {
        private readonly IMemoryCache _cache;
        private readonly CatalogService _catalogService;
        private static readonly string _brandsKey = "brands"; // @issue@I02
        private static readonly string _typesKey = "types"; // @issue@I02
        private static readonly string _itemsKeyTemplate = "items-{0}-{1}-{2}-{3}"; // @issue@I02
        private static readonly TimeSpan _defaultCacheDuration = TimeSpan.FromSeconds(30); // @issue@I02

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
                        entry.SlidingExpiration = _defaultCacheDuration; // @issue@I02
                return await _catalogService.GetBrands(); // @issue@I02
            });
        }

        public async Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandID, int? typeId) // @issue@I02
        {
            string cacheKey = String.Format(_itemsKeyTemplate, pageIndex, itemsPage, brandID, typeId); // @issue@I02
            return await _cache.GetOrCreateAsync(cacheKey, async entry => // @issue@I02
            {
                entry.SlidingExpiration = _defaultCacheDuration; // @issue@I02
                return await _catalogService.GetCatalogItems(pageIndex, itemsPage, brandID, typeId); // @issue@I02
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetTypes() // @issue@I02
        {
            return await _cache.GetOrCreateAsync(_typesKey, async entry => // @issue@I02
            {
                entry.SlidingExpiration = _defaultCacheDuration; // @issue@I02
                return await _catalogService.GetTypes(); // @issue@I02
            });
        }
    }
}
