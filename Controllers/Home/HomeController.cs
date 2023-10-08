using Microsoft.AspNetCore.Mvc;
using SpeedDeal.Models;
using SpeedDeal.DbModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;

namespace SpeedDeal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private AppDbContext _dbContext;
        public HomeController(ILogger<HomeController> logger,
                 AppDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _dbContext = context;
        }

       
        public IActionResult Index()
        {
            var user = HttpContext.User;
           
            if (null != user)  
            {  
                var dbUser = _dbContext.Users.Include(g=>g.Group).FirstOrDefault(u => u.Name == user.Identity.Name);
                Console.WriteLine(dbUser.Password);
                
                foreach (Claim claim in user.Claims)  
                {  
                    Console.WriteLine("CLAIM TYPE: " + claim.Type + "; CLAIM VALUE: " + claim.Value + "</br>");  
                }  

            }  

            return View();
        }

        [Authorize(Policy = "AdminOnly")]
        public string amdin()
        {
            return "ok";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}