namespace SpeedDeal.Controllers
{
    public class ControlPanelController : Controller
    {
        private AppDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        public ControlPanelController(ILogger<HomeController> logger,
                 AppDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }

        public async Task<IActionResult> Index(){
            
        }
        }
    }
}