using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using SpeedDeal.Infrastructure;


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
    [Route("/login")]
    public IActionResult Index()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(string? returnUrl, string name, string password)
    {
        var user = _dbContext.Users.Include(g=>g.Group).FirstOrDefault(u=>u.Name == name);
        if(user == null) return RedirectToAction("UserNotFound");
        if (user.Password != password) return RedirectToAction("WrongPassword");

        var hash = Hasher.CreateHash(user.Password);
        Console.Write(hash);
        Console.WriteLine( Hasher.ValidatePassword(user.Password,Console.ReadLine()));
        
        var claims = new List<Claim> { new Claim(ClaimTypes.Name , user.Name) };
        claims.Add(new Claim(user.Group.Name, "admin"));
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity)); 
        
        return RedirectToAction("Index","Home");
    }

    [Authorize]
    [Route("/logout")]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
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
    
    string HashPasword(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(64);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            350000,
            HashAlgorithmName.SHA512,
            64);
        return Convert.ToHexString(hash);
    }
}
