using HomeAssets.Models;
using HomeAssets.Models.DataBaseContext;
using HomeAssets.Models.ExtendedIdentity;
using HomeAssets.Security;
using HomeAssets.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

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
            services.Configure<SmtpSettings>(config.GetSection("SmtpSettings"));
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(15);
            });
            services.Configure<CustomEmailConfirmationTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromDays(2);
            });

            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("HomeServiceDB"));
            });

            services.AddIdentity<App_IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;

                options.SignIn.RequireConfirmedEmail = true;

                options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmationTokenProvider";
            }).AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders()
              .AddTokenProvider<CustomEmailConfirmationTokenProvider<App_IdentityUser>>("CustomEmailConfirmationTokenProvider");

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
            services.AddTransient<IMailService, EmailService>();
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