using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Microsoft.eShopWeb.RazorPages.Pages.Account.Manage
{
    public static class ManageNavPages
    {
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
            var activePage = viewContext.ViewData["ActivePage"] as string // @issue@I02
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName); // @issue@I02
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null; // @issue@I02
        }
    }
}
