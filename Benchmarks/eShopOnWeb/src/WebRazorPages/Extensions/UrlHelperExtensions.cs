namespace Microsoft.AspNetCore.Mvc
{
    public static class UrlHelperExtensions
    {
        public static string GetLocalUrl(this IUrlHelper urlHelper, string localUrl) // @issue@I02
        {
            if (!urlHelper.IsLocalUrl(localUrl)) // @issue@I02
            {
                return urlHelper.Page("/Index"); // @issue@I02
            }

            return localUrl; // @issue@I02
        }

        public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme) // @issue@I02
        {
            return urlHelper.Page( // @issue@I02
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId, code },
                protocol: scheme);
        }

        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string code, string scheme) // @issue@I02
        {
            return urlHelper.Page( // @issue@I02
                "/Account/ResetPassword",
                pageHandler: null,
                values: new { userId, code },
                protocol: scheme);
        }
    }
}
