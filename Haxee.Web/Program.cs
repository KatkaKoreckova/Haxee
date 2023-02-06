using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

            builder.Services.AddDbContext<DataContext>(options => 
            {
                options.UseInMemoryDatabase("MainDB");
            });

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
            })
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

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

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}