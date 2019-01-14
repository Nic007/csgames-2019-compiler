using System.ComponentModel.DataAnnotations;

namespace Microsoft.eShopWeb.Web.ViewModels.Manage
{
    public class TwoFactorAuthenticationViewModel
    {
        public bool HasAuthenticator { get; set; } // @issue@I02
        public int RecoveryCodesLeft { get; set; } // @issue@I02
        public bool Is2faEnabled { get; set; } // @issue@I02
    }
}
