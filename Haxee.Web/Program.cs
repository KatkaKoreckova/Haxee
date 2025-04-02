using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using Radzen;

namespace Haxee.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            builder.Services.AddDbContext<DataContext>(
                options =>
                {
                    options.UseInMemoryDatabase("MainDB");
                },
                ServiceLifetime.Singleton
            );

            builder.Services.AddDbContextFactory<DataContext>(options =>
            {
                options.UseInMemoryDatabase("MainDB");
            });

            builder
                .Services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.SignIn.RequireConfirmedEmail = false;
                })
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    Constants.Policies.RequireAdmin,
                    policy => policy.RequireClaim(ClaimTypes.Role, Constants.Roles.Admin).Build()
                );
                options.AddPolicy(
                    Constants.Policies.RequireUser,
                    policy => policy.RequireClaim(ClaimTypes.Role, Constants.Roles.User).Build()
                );
            });

            builder.Services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 7044;
            });

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Identity";
                config.LoginPath = "/auth/login";
                config.AccessDeniedPath = "/auth/login";
            });

            builder.Services.AddHttpClient();

            builder.Services.AddControllers();

            var mailKitOptions = builder.Configuration.GetSection("Email").Get<MailKitOptions>();
            builder.Services.AddMailKit(config => config.UseMailKit(mailKitOptions));

            builder.Services.AddScoped<MailService>();
            builder.Services.AddHostedService<StandVisitExpiryHostedService>();

            builder.Services.AddRadzenComponents();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                DataSeeder.Seed(scope.ServiceProvider).GetAwaiter().GetResult();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
