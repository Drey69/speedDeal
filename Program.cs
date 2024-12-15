using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SpeedDeal.Infrastructure;
using Microsoft.Extensions.Options;


namespace SpeedDeal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();

            var dbContext = new AppDbContext();
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            if (!dbContext.Users.Any()) 
            {
                dbContext.Users.Add(new DbModels.User
                {
                    Name = "admin",
                    Password = Hasher.HashPasword("admin", out var salt),
                    Salt = salt,
                    Role = new DbModels.Role 
                    {
                        Name= "admin"
                    }
                }); 
                dbContext.SaveChanges();
            }

            builder.Configuration.AddJsonFile("appsettings.json");
            builder.Services.AddSingleton(config);
             builder.Services.AddSingleton(dbContext);


            builder.Services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.AccessDeniedPath = "/accessdenied";
                });

            builder.Services.AddAuthorization();

            var app = builder.Build();

   

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapDefaultControllerRoute();

         //   app.MapControllerRoute(
         //       name: "default",
           //     pattern: "{controller=Home}/{action=Index}/{id?}");
        
            
            app.Run();
        }
    }
}



