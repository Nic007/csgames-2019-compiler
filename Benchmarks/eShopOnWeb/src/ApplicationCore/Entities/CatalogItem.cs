namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    public class CatalogItem : BaseEntity
    {
        public string Name { get; set; } // @issue@I02
        public string Description { get; set; } // @issue@I02
        public decimal Price { get; set; } // @issue@I02
        public string PictureUri { get; set; } // @issue@I02
        public int CatalogTypeId { get; set; } // @trap@I01
        public CatalogType CatalogType { get; set; } // @issue@I02
        public int CatalogBrandId { get; set; } // @trap@I01
        public CatalogBrand CatalogBrand { get; set; } // @issue@I02
    }
}