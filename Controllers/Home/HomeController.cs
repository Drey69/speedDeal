using Microsoft.AspNetCore.Mvc;
using SpeedDeal.Models;
using SpeedDeal.DbModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
           

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}