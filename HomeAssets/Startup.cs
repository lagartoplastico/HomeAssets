using HomeAssets.Models;
using HomeAssets.Models.DataBaseContext;
using HomeAssets.Models.ExtendedIdentity;
using HomeAssets.Security;
using HomeAssets.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
                options.UseNpgsql(config.GetConnectionString("HomeAssetsDB"));
            });

            services.AddIdentity<App_IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;

                options.SignIn.RequireConfirmedEmail = true;

                options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmationTokenProvider";

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            }).AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders()
              .AddTokenProvider<CustomEmailConfirmationTokenProvider<App_IdentityUser>>("CustomEmailConfirmationTokenProvider");

            services.AddControllersWithViews();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "408086448053-bh9tgdh72nlo48lm58ci4ahfdk2b1ec3.apps.googleusercontent.com";
                    options.ClientSecret = "GWYSt42gcJ9nH41Qr1Db-96b";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AccountManagers",
                    policy => policy.RequireClaim("Role", new string[] { "admin1" }));
                options.AddPolicy("AccountViewers",
                    policy => policy.RequireClaim("Role", new string[] { "admin2",
                                                                         "admin1"}));
                options.AddPolicy("ServiceManagers",
                    policy => policy.RequireClaim("Role", new string[] { "user1",
                                                                         "admin2",
                                                                         "admin1"}));
                options.AddPolicy("ServiceViewers",
                    policy => policy.RequireClaim("Role", new string[] { "user2",
                                                                         "user1",
                                                                         "admin2",
                                                                         "admin1"}));
            });

            services.AddScoped<IHomeServiceRepo, NpgsqlHomeServiceRepo>();
            services.AddScoped<IAuthorizedEmailRepo, NpgsqlAuthorizedEmailRepo>();
            services.AddTransient<IMailService, EmailService>();
            services.AddSingleton<DataProtectionPurposeStrings>();
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}