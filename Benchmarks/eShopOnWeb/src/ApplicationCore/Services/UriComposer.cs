using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb;

namespace Microsoft.eShopWeb.ApplicationCore.Services
{
    public class UriComposer : IUriComposer
    {
        private readonly CatalogSettings _catalogSettings;  // @issue@I03
        private readonly CatalogSettings _catalogSettings2; // @issue@I02 // @trap@I03

        public UriComposer(CatalogSettings catalogSettings) => _catalogSettings = catalogSettings; // @issue@I02

        public string ComposePicUri(string uriTemplate) // @issue@I02
        {
            return uriTemplate.Replace("http://catalogbaseurltobereplaced", _catalogSettings.CatalogBaseUrl); // @issue@I02
        }
    }
}
