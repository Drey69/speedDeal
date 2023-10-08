using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;


namespace SpeedDeal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            var dbContext = new AppDbContext();
            IConfiguration config = new ConfigurationBuilder()
         .AddJsonFile("appsettings.json")
         .AddEnvironmentVariables()
         .Build();


                
                    
                    
                    
            // Add services to the container.
             

            builder.Configuration.AddJsonFile("appsettings.json");
            builder.Services.AddSingleton(config);
            builder.Services.AddSingleton(dbContext);

            
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => options.LoginPath = "/login");
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
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Map("/test", [Authorize]() => $"Hello World!");
            app.Map("/", () => "Home Page");
            
            app.Run();
        }
    }
}


