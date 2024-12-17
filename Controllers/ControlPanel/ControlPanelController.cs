using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using SpeedDeal.Controllers.ControlPanel.ViewModels;
using SpeedDeal.DbModels;
using SpeedDeal.Models;
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

        public  IActionResult Index()
        {
            var links = new List<ControlPanelPageItem>();
            var user = new User();
            if(Context.User.IsInRole("admin"))
            {
                links.Add(new ControlPanelPageItem 
                {
                    Name = "Роли", 
                    Link = "/ControlPanel/Roles",
                    Role = "admin"
                });
            }
            
            
            //links.Add(new ControlPanelPageItem { Name = "������",  Link = "/ControlPanel/Claims"});

            var model = new ControlPanelViewModel(user, links.OrderBy(l => l.Name).ToList());
            return View(model);
        }
        


        public IActionResult Roles() 
        {
            var roles = _context.Roles.ToList();
            return View("/Views/ControlPanel/Roles/Roles.cshtml", roles);
        }

        [HttpGet]
        public IActionResult RoleEdit(int roleId)
        {
            var role = _context.Roles
                .AsNoTracking()
                .FirstOrDefault(r => r.Id == roleId);

            if (role == null)
            {
                return View("Error", new ErrorViewModel { RequestId = "���� �� ��������" });
            }
            return View("/Views/ControlPanel/Roles/RoleEdit.cshtml", role);
        }

        [HttpPost]
        public IActionResult RoleEdit(string roleName, int roleId)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return View("Error", new ErrorViewModel { RequestId = $"��� ���� ������" });
            }

            if (roleId > 0)
            {
                var myRole = _context.Roles.FirstOrDefault(r => r.Id == roleId);
                if (myRole == null) {
                    return View("Error", new ErrorViewModel { RequestId = $"����  {roleId} �� ��������" });
                }
                myRole.Name = roleName;

                _context.SaveChanges();
                return RedirectToAction("Roles");
            }

            var newRole = new Role(roleName);
            _context.Roles.Add(newRole);
            _context.SaveChanges();

            return RedirectToAction("Roles");            
        }

        public IActionResult RoleCreate()
        {
            var model = new Role();
            return View("/Views/ControlPanel/Roles/RoleEdit.cshtml", model);
        }
    }
}