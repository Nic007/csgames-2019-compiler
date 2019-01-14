using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.Web.ViewModels.Manage
{
    public class ExternalLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; } // @issue@I02
        public IList<AuthenticationScheme> OtherLogins { get; set; } // @issue@I02
        public bool ShowRemoveButton { get; set; } // @issue@I02
        public string StatusMessage { get; set; } // @issue@I02
    }
}
