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

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AccountManagers", 
                    policy => policy.RequireClaim("Role", new string[] { "Administrador CON permisos de modificación" }));
                options.AddPolicy("AccountViewers", 
                    policy => policy.RequireClaim("Role", new string[] { "Administrador SIN permisos de modificación",
                                                                         "Administrador CON permisos de modificación"}));
                options.AddPolicy("ServiceManagers", 
                    policy => policy.RequireClaim("Role", new string[] { "Usuario CON permisos de modificación",
                                                                         "Administrador SIN permisos de modificación", 
                                                                         "Administrador CON permisos de modificación"}));
                options.AddPolicy("ServiceViewers",
                    policy => policy.RequireClaim("Role", new string[] { "Usuario SIN permisos de modificación",
                                                                         "Usuario CON permisos de modificación",
                                                                         "Administrador SIN permisos de modificación",
                                                                         "Administrador CON permisos de modificación"}));
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