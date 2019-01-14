using System.ComponentModel.DataAnnotations;

namespace Microsoft.eShopWeb.Web.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } // @issue@I02

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } // @issue@I02

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; } // @issue@I02
    }
}
