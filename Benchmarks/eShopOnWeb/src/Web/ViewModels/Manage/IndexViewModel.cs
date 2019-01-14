using System.ComponentModel.DataAnnotations;

namespace Microsoft.eShopWeb.Web.ViewModels.Manage
{
    public class IndexViewModel
    {
        public string Username { get; set; } // @issue@I02

        public bool IsEmailConfirmed { get; set; } // @issue@I02

        [Required]
        [EmailAddress]
        public string Email { get; set; } // @issue@I02

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } // @issue@I02

        public string StatusMessage { get; set; } // @issue@I02
    }
}
