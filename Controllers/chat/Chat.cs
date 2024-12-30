using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SpeedDeal.Services;

namespace SpeedDeal.Controllers.chat;


public class ChatController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;
    private AppDbContext _dbContext;
    private ChatHub _chatHub;
    public ChatController(AppDbContext context, ILogger<HomeController> logger, IConfiguration configuration, AppDbContext dbContext, ChatHub chatHub)
    {
        _logger = logger;
        _configuration = configuration;
        _dbContext = context;
        _chatHub = chatHub;
    }


    
    
    public IActionResult Index()
    {
        var r = _chatHub;
        
        ViewBag.test = "htllo mazafaka";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Send ([FromQuery]string message)
    {
        var userName = HttpContext.User.Identity?.Name ?? "_user_";
        await _chatHub.Clients.All.SendAsync("Receive", $"{userName}: {message}");
        return Content("Ok");
    }
}