using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Infrastructure.Logging;
using Microsoft.eShopWeb.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.RazorPages.Interfaces;
using Microsoft.eShopWeb.RazorPages.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

namespace Microsoft.eShopWeb.RazorPages
{
    public class Startup
    {
        private IServiceCollection _services;
        public Startup(IConfiguration configuration) // @issue@I02
        {
            Configuration = configuration; // @issue@I02
        }

        public IConfiguration Configuration { get; } // @issue@I02 // @trap@I03

        public void ConfigureDevelopmentServices(IServiceCollection services) // @issue@I02
        {
            // use in-memory database
            ConfigureTestingServices(services); // @issue@I02

            // use real database
            // ConfigureProductionServices(services); // @issue@I06

        }
        public void ConfigureTestingServices(IServiceCollection services) // @issue@I02
        {
            // use in-memory database(this is not really a parameter) // @trap@I06
            services.AddDbContext<CatalogContext>(c => // @issue@I02
                c.UseInMemoryDatabase("Catalog")); // @issue@I02

            // Add Identity DbContext
            services.AddDbContext<AppIdentityDbContext>(options => // @issue@I02
                options.UseInMemoryDatabase("Identity")); // @issue@I02

            ConfigureServices(services); // @issue@I02
        }

        public void ConfigureProductionServices(IServiceCollection services) // @issue@I02
        {
            // use real database
            services.AddDbContext<CatalogContext>(c => // @issue@I02
            {
                try
                {
                    // Requires LocalDB which can be installed with SQL Server Express 2016
                    // https://www.microsoft.com/en-us/download/details.aspx?id=54284
                    c.UseSqlServer(Configuration.GetConnectionString("CatalogConnection")); // @issue@I02
                }
                catch (System.Exception ex)
                {
                    var message = ex.Message; // @issue@I02 // @issue@I03
                }
            });

            // Add Identity DbContext
            services.AddDbContext<AppIdentityDbContext>(options => // @issue@I02
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"))); // @issue@I02

            ConfigureServices(services); // @issue@I02
        }

        public void ConfigureServices(IServiceCollection services) // @issue@I02 // @issue@I05
        {
            services.AddIdentity<ApplicationUser, IdentityRole>() // @issue@I02
                .AddEntityFrameworkStores<AppIdentityDbContext>() // @issue@I02
                .AddDefaultTokenProviders(); // @issue@I02

            services.ConfigureApplicationCookie(options => // @issue@I02
            {
                options.Cookie.HttpOnly = true; // @issue@I02
                options.ExpireTimeSpan = TimeSpan.FromHours(1); // @issue@I02
                options.LoginPath = "/Account/Signin"; // @issue@I02
                options.LogoutPath = "/Account/Signout"; // @issue@I02
            });

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>)); // @issue@I02
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>)); // @issue@I02

            services.AddScoped<ICatalogService, CachedCatalogService>(); // @issue@I02
            services.AddScoped<IBasketService, BasketService>(); // @issue@I02
            services.AddScoped<IBasketViewModelService, BasketViewModelService>(); // @issue@I02
            services.AddScoped<IOrderService, OrderService>(); // @issue@I02
            services.AddScoped<IOrderRepository, OrderRepository>(); // @issue@I02
            services.AddScoped<CatalogService>(); // @issue@I02
            services.Configure<CatalogSettings>(Configuration); // @issue@I02
            services.AddSingleton<IUriComposer>(new UriComposer(Configuration.Get<CatalogSettings>())); // @issue@I02

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>)); // @issue@I02
            services.AddTransient<IEmailSender, EmailSender>(); // @issue@I02

            // Add memory cache services
            services.AddMemoryCache(); // @issue@I02

            services.AddMvc() // @issue@I02
                .SetCompatibilityVersion(AspNetCore.Mvc.CompatibilityVersion.Version_2_1) // @issue@I02
                .AddRazorPagesOptions(options => // @issue@I02
                {
                    options.Conventions.AuthorizeFolder("/Order"); // @issue@I02
                    options.Conventions.AuthorizePage("/Basket/Checkout"); // @issue@I02
                });

            _services = services; // @issue@I02
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, // @issue@I02
            IHostingEnvironment env)
        {
            if (env.IsDevelopment()) // @issue@I02
            {
                app.UseDeveloperExceptionPage(); // @issue@I02
                ListAllRegisteredServices(app); // @issue@I02
                app.UseDatabaseErrorPage(); // @issue@I02
            }
            else
            {
                app.UseExceptionHandler("/Catalog/Error"); // @issue@I02
                app.UseHsts(); // @issue@I02
            }

            app.UseHttpsRedirection(); // @issue@I02
            app.UseStaticFiles(); // @issue@I02
            app.UseAuthentication(); // @issue@I02

            app.UseMvc(); // @issue@I02
        }

        private void ListAllRegisteredServices(IApplicationBuilder app) // @issue@I02
        {
            app.Map("/allservices", builder => builder.Run(async context => // @issue@I02
            {
                var sb = new StringBuilder(); // @issue@I02
                sb.Append("<h1>All Services</h1>"); // @issue@I02
                sb.Append("<table><thead>"); // @issue@I02
                sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>"); // @issue@I02
                sb.Append("</thead><tbody>"); // @issue@I02
                foreach (var svc in _services) // @issue@I02
                {
                    sb.Append("<tr>"); // @issue@I02
                    sb.Append($"<td>{svc.ServiceType.FullName}</td>"); // @issue@I02
                    sb.Append($"<td>{svc.Lifetime}</td>"); // @issue@I02
                    sb.Append($"<td>{svc.ImplementationType?.FullName}</td>"); // @issue@I02
                    sb.Append("</tr>"); // @issue@I02
                }
                sb.Append("</tbody></table>"); // @issue@I02
                await context.Response.WriteAsync(sb.ToString()); // @issue@I02
            }));
        }
    }
}
