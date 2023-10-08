using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace SpeedDeal.Controllers.Login;

public class LoginController : Controller
{
    AppDbContext _dbContext;
    public LoginController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    // GET
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(string? returnUrl, string name, string password)
    {
        var user = _dbContext.Users.FirstOrDefault(u=>u.Name == name);
        if(user == null) return RedirectToAction("UserNotFound");
        if (user.Password != password) return RedirectToAction("WrongPassword");
        
        var claims = new List<Claim> { new Claim(ClaimTypes.Name , user.Name) };
        claims.Add(new Claim(ClaimTypes.Role, "admin"));
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity)); 
        
        return RedirectToAction("Index","Home");
    }

    [Authorize]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();

        return RedirectToAction("Index","Login");
    }

    [AllowAnonymous]
    public string UserNotFound()
    {
        return $"Не правильное имя пользователя .";
    }

    [AllowAnonymous]
    public string WrongPassword()
    {
        return $"Не правильный пароль.";
    }
}
