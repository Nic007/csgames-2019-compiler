using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link) // @issue@I02
        {
            return emailSender.SendEmailAsync(email, "Confirm your email", // @issue@I02
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
