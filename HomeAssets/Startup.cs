using HomeAssets.Models;
using HomeAssets.Models.DataBaseContext;
using HomeAssets.Models.ExtendedIdentity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HomeAssets
{
    public class Startup
    {
        private readonly IConfiguration config;

        public Startup(IConfiguration config)
        {
            this.config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("HomeServiceDB"));
            });

            services.AddIdentity<App_IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
            }).AddEntityFrameworkStores<AppDbContext>();

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;

                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "408086448053-bh9tgdh72nlo48lm58ci4ahfdk2b1ec3.apps.googleusercontent.com";
                    options.ClientSecret = "GWYSt42gcJ9nH41Qr1Db-96b";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AccountManagers", 
                    policy => policy.RequireClaim("Role", new string[] { "Administrador CON permisos de modificaci�n" }));
                options.AddPolicy("AccountViewers", 
                    policy => policy.RequireClaim("Role", new string[] { "Administrador SIN permisos de modificaci�n",
                                                                         "Administrador CON permisos de modificaci�n"}));
                options.AddPolicy("ServiceManagers", 
                    policy => policy.RequireClaim("Role", new string[] { "Usuario CON permisos de modificaci�n",
                                                                         "Administrador SIN permisos de modificaci�n", 
                                                                         "Administrador CON permisos de modificaci�n"}));
                options.AddPolicy("ServiceViewers",
                    policy => policy.RequireClaim("Role", new string[] { "Usuario SIN permisos de modificaci�n",
                                                                         "Usuario CON permisos de modificaci�n",
                                                                         "Administrador SIN permisos de modificaci�n",
                                                                         "Administrador CON permisos de modificaci�n"}));
            });

            //services.AddSingleton<IHomeServiceRepo, MockHomeServiceRepo>();
            services.AddScoped<IHomeServiceRepo, NpgsqlHomeServiceRepo>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}