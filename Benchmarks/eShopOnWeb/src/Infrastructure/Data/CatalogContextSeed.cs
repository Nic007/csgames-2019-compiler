using Microsoft.AspNetCore.Builder;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Infrastructure.Data
{
    public class CatalogContextSeed
    {
        public static async Task SeedAsync(CatalogContext catalogContext, // @issue@I02 // @issue@I05
            ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value; // @issue@I02
            try
            {
                // TODO: Only run this if using a real database
                // context.Database.Migrate(); // @issue@I06

                if (!catalogContext.CatalogBrands.Any()) // @issue@I02
                {
                    catalogContext.CatalogBrands.AddRange( // @issue@I02
                        GetPreconfiguredCatalogBrands());

                    await catalogContext.SaveChangesAsync(); // @issue@I02
                }

                if (!catalogContext.CatalogTypes.Any()) // @issue@I02
                {
                    catalogContext.CatalogTypes.AddRange( // @issue@I02
                        GetPreconfiguredCatalogTypes());

                    await catalogContext.SaveChangesAsync(); // @issue@I02
                }

                if (!catalogContext.catalogItems.Any()) // @issue@I02
                {
                    catalogContext.catalogItems.AddRange( // @issue@I02
                        GetPreconfiguredItems());

                    await catalogContext.SaveChangesAsync(); // @issue@I02
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10) // @issue@I02
                {
                    retryForAvailability++; // @issue@I02
                    var log = loggerFactory.CreateLogger<CatalogContextSeed>(); // @issue@I02
                    log.LogError(ex.Message); // @issue@I02
                    await SeedAsync(catalogContext, loggerFactory, retryForAvailability); // @issue@I02
                }
            }
        }

        static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands() // @issue@I02
        {
            return new List<CatalogBrand>() // @issue@I02
            {
                new CatalogBrand() { Brand = "Azure"},
                new CatalogBrand() { Brand = ".NET" },
                new CatalogBrand() { Brand = "Visual Studio" },
                new CatalogBrand() { Brand = "SQL Server" }, 
                new CatalogBrand() { Brand = "Other" }
            };
        }

        static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes() // @issue@I02
        {
            return new List<CatalogType>() // @issue@I02
            {
                new CatalogType() { Type = "Mug"},
                new CatalogType() { Type = "T-Shirt" },
                new CatalogType() { Type = "Sheet" },
                new CatalogType() { Type = "USB Memory Stick" }
            };
        }

        static IEnumerable<CatalogItem> GetPreconfiguredItems() // @issue@I02
        {
            return new List<CatalogItem>() // @issue@I02
            {
                new CatalogItem() { CatalogTypeId=2,CatalogBrandId=2, Description = ".NET Bot Black Sweatshirt", Name = ".NET Bot Black Sweatshirt", Price = 19.5M, PictureUri = "http://catalogbaseurltobereplaced/images/products/1.png" },
                new CatalogItem() { CatalogTypeId=1,CatalogBrandId=2, Description = ".NET Black & White Mug", Name = ".NET Black & White Mug", Price= 8.50M, PictureUri = "http://catalogbaseurltobereplaced/images/products/2.png" },
                new CatalogItem() { CatalogTypeId=2,CatalogBrandId=5, Description = "Prism White T-Shirt", Name = "Prism White T-Shirt", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/3.png" },
                new CatalogItem() { CatalogTypeId=2,CatalogBrandId=2, Description = ".NET Foundation Sweatshirt", Name = ".NET Foundation Sweatshirt", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/4.png" },
                new CatalogItem() { CatalogTypeId=3,CatalogBrandId=5, Description = "Roslyn Red Sheet", Name = "Roslyn Red Sheet", Price = 8.5M, PictureUri = "http://catalogbaseurltobereplaced/images/products/5.png" },
                new CatalogItem() { CatalogTypeId=2,CatalogBrandId=2, Description = ".NET Blue Sweatshirt", Name = ".NET Blue Sweatshirt", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/6.png" },
                new CatalogItem() { CatalogTypeId=2,CatalogBrandId=5, Description = "Roslyn Red T-Shirt", Name = "Roslyn Red T-Shirt", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/7.png"  },
                new CatalogItem() { CatalogTypeId=2,CatalogBrandId=5, Description = "Kudu Purple Sweatshirt", Name = "Kudu Purple Sweatshirt", Price = 8.5M, PictureUri = "http://catalogbaseurltobereplaced/images/products/8.png" },
                new CatalogItem() { CatalogTypeId=1,CatalogBrandId=5, Description = "Cup<T> White Mug", Name = "Cup<T> White Mug", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/9.png" },
                new CatalogItem() { CatalogTypeId=3,CatalogBrandId=2, Description = ".NET Foundation Sheet", Name = ".NET Foundation Sheet", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/10.png" },
                new CatalogItem() { CatalogTypeId=3,CatalogBrandId=2, Description = "Cup<T> Sheet", Name = "Cup<T> Sheet", Price = 8.5M, PictureUri = "http://catalogbaseurltobereplaced/images/products/11.png" },
                new CatalogItem() { CatalogTypeId=2,CatalogBrandId=5, Description = "Prism White TShirt", Name = "Prism White TShirt", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/12.png" }
            };
        }
    }
}
