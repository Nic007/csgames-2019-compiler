using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager) // @issue@I02
        {
            var defaultUser = new ApplicationUser { UserName = "demouser@microsoft.com", Email = "demouser@microsoft.com" }; // @issue@I02
            await userManager.CreateAsync(defaultUser, "Pass@word1"); // @issue@I02
        }
    }
}
