using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using SpeedDeal.Controllers.ControlPanel.ViewModels;
using SpeedDeal.DbModels;
using SpeedDeal.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using SpeedDeal.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace SpeedDeal.Controllers
{
    [Authorize]
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
            if(HttpContext.User.IsInRole("admin"))
            {
                links.Add(new ControlPanelPageItem 
                {
                    Name = "Роли", 
                    Link = "/ControlPanel/Roles"
                });
            }
            
            links.Add(new ControlPanelPageItem 
            {
                Name = "Смена пароля", 
                Link = "/ControlPanel/ChangePassword"
            });
            links.Add(new ControlPanelPageItem 
            {
                Name = "Смена цветов", 
                Link = "/ControlPanel/ChangeColors"
            });
            
            //links.Add(new ControlPanelPageItem { Name = "������",  Link = "/ControlPanel/Claims"});

            var model = new ControlPanelViewModel(user, links.OrderBy(l => l.Name).ToList());
            return View(model);
        }
        
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(string name, string aldPassword,
                    string  newPassword)
        {

            var userIdStr = HttpContext.User.Claims.First(c => c.Type == "UserId").Value;
            if(userIdStr == null)
            {
                return View("Error", new ErrorViewModel {
                        RequestId = "Что то пошло нетак claim userId not found"});
            }

            if(string.IsNullOrWhiteSpace(name) ||
               string.IsNullOrWhiteSpace(newPassword) ||
               string.IsNullOrWhiteSpace(aldPassword))
            {
                return View("Error", new ErrorViewModel {
                        RequestId = "Все поля должны быть заполнены"});
            }


            int.TryParse(userIdStr, out var userId);

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if(user == null)
            {
                 return View("Error", new ErrorViewModel {
                        RequestId = "прользователь не найден"});
            }
            
            if(!Hasher.IsPaswordOk(aldPassword, user.Password, user.Salt))
            {
                return View("Error",
                 new ErrorViewModel { RequestId = "Не верный старый пароль" });
            }

            user.Name = name;
            user.Password = Hasher.HashPaswordBySalt(newPassword, user.Salt);

            _context.SaveChanges();

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Login");
        }

        [Authorize(Roles = "admin")]
        public IActionResult Roles() 
        {
            var roles = _context.Roles.ToList();
            return View("/Views/ControlPanel/Roles/Roles.cshtml", roles);
        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        public IActionResult RoleCreate()
        {
            var model = new Role();
            return View("/Views/ControlPanel/Roles/RoleEdit.cshtml", model);
        }
    }
}