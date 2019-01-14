using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Microsoft.eShopWeb.Web.Views.Manage
{
    public static class ManageNavPages
    {
        public static string ActivePageKey => "ActivePage"; // @issue@I02

        public static string Index => "Index"; // @issue@I02

        public static string ChangePassword => "ChangePassword"; // @issue@I02

        public static string ExternalLogins => "ExternalLogins"; // @issue@I02

        public static string TwoFactorAuthentication => "TwoFactorAuthentication"; // @issue@I02

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index); // @issue@I02

        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword); // @issue@I02

        public static string ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins); // @issue@I02

        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication); // @issue@I02

        public static string PageNavClass(ViewContext viewContext, string page) // @issue@I02
        {
            var activePage = viewContext.ViewData["ActivePage"] as string; // @issue@I02
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null; // @issue@I02
        }

        public static void AddActivePage(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage; // @issue@I02
    }
}
