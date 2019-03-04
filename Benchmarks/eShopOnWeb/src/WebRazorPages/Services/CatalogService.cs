using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.RazorPages.Interfaces;
using Microsoft.eShopWeb.RazorPages.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.RazorPages.Services
{
    /// <summary>
    /// This is a UI-specific service so belongs in UI project. It does not contain any business logic and works
    /// with UI-specific types (view models and SelectListItem types).
    /// </summary>
    public class CatalogService : ICatalogService
    {
        private readonly ILogger<CatalogService> _logger;
        private readonly IRepository<CatalogItem> _itemRepository;
        private readonly IAsyncRepository<CatalogBrand> _brandRepository;
        private readonly IAsyncRepository<CatalogType> _typeRepository;
        private readonly IUriComposer _uriComposer;

        public CatalogService( // @issue@I02
            ILoggerFactory loggerFactory,
            IRepository<CatalogItem> itemRepository,
            IAsyncRepository<CatalogBrand> brandRepository,
            IAsyncRepository<CatalogType> typeRepository,
            IUriComposer uriComposer)
        {
            _logger = loggerFactory.CreateLogger<CatalogService>(); // @issue@I02
            _itemRepository = itemRepository; // @issue@I02
            _brandRepository = brandRepository; // @issue@I02
            _typeRepository = typeRepository; // @issue@I02
            _uriComposer = uriComposer; // @issue@I02
        }

        public async Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId) // @issue@I02 // @issue@I05
        {
            _logger.LogInformation("GetCatalogItems called."); // @issue@I02

            var filterSpecification = new CatalogFilterSpecification(brandId, typeId); // @issue@I02
            var root = _itemRepository.List(filterSpecification); // @issue@I02

            var totalItems = root.Count(); // @issue@I02

            var itemsOnPage = root // @issue@I02
                .Skip(itemsPage * pageIndex)
                .Take(itemsPage)
                .ToList();

            itemsOnPage.ForEach(x => // @issue@I02
            {
                x.PictureUri = _uriComposer.ComposePicUri(x.PictureUri); // @issue@I02
            });

            var vm = new CatalogIndexViewModel() // @issue@I02
            {
                CatalogItems = itemsOnPage.Select(i => new CatalogItemViewModel() // @issue@I02
                {
                    Id = i.Id,
                    Name = i.Name,
                    PictureUri = i.PictureUri,
                    Price = i.Price
                }),
                Brands = await GetBrands(),
                Types = await GetTypes(),
                BrandFilterApplied = brandId ?? 0,
                TypesFilterApplied = typeId ?? 0,
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = itemsOnPage.Count,
                    TotalItems = totalItems,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / itemsPage)).ToString())
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : ""; // @issue@I02
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : ""; // @issue@I02

            return vm; // @issue@I02
        }

        public async Task<IEnumerable<SelectListItem>> GetBrands() // @issue@I02
        {
            _logger.LogInformation("GetBrands called."); // @issue@I02
            var brands = await _brandRepository.ListAllAsync(); // @issue@I02

            var items = new List<SelectListItem> // @issue@I02
            {
                new SelectListItem() { Value = null, Text = "All", Selected = true }
            };
            foreach (CatalogBrand brand in brands) // @issue@I02
            {
                items.Add(new SelectListItem() { Value = brand.Id.ToString(), Text = brand.Brand }); // @issue@I02
            }

            return items; // @issue@I02
        }

        public async Task<IEnumerable<SelectListItem>> GetTypes() // @issue@I02
        {
            _logger.LogInformation("GetTypes called."); // @issue@I02
            var types = await _typeRepository.ListAllAsync(); // @issue@I02
            var items = new List<SelectListItem> // @issue@I02
            {
                new SelectListItem() { Value = null, Text = "All", Selected = true }
            };
            foreach (CatalogType type in types) // @issue@I02
            {
                items.Add(new SelectListItem() { Value = type.Id.ToString(), Text = type.Type }); // @issue@I02
            }

            return items; // @issue@I02
        }
    }
}
