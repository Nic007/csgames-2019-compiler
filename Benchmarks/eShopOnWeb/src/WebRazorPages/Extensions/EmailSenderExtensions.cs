using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link) // @issue@I02
        {
            return emailSender.SendEmailAsync(email, "Confirm your email", // @issue@I02
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(link)}'>clicking here</a>."); // @issue@I02
        }

        public static Task SendResetPasswordAsync(this IEmailSender emailSender, string email, string callbackUrl) // @issue@I02
        {
            return emailSender.SendEmailAsync(email, "Reset Password", // @issue@I02
                $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."); // @issue@I02
        }
    }

}
