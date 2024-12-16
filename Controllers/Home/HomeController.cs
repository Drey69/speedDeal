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

        public IActionResult GetVue()
        {
            var filepath = "/js/test.vue";
            return File(filepath, "text/plain", "test.vue");
        }

       
        public IActionResult Index()
        {


            return View();
        }


        [Authorize(Roles = "admin")]
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