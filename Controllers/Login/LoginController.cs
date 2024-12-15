using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using SpeedDeal.Infrastructure;
using System.Runtime.InteropServices;
using System;


namespace SpeedDeal.Controllers.Login;

public class LoginController : Controller
{
    AppDbContext _context;
    public LoginController(AppDbContext dbContext)
    {
        _context = dbContext;
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
        var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Name == name);
        if(user == null)
        {
            RedirectToAction("UserNotFound", "Login");
        }

        if(Hasher.IsPaswordOk(password, user.Password, user.Salt))
        {
            var claims = new List<Claim>
            {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
    };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);
            return RedirectToAction("index", "Home");
        }
     
        return RedirectToAction("Login", "Login");
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
        return $"Не правильное имя пользователя или пароль .";
    }

    [AllowAnonymous]
    public string WrongPassword()
    {
        return $"Не правильное имя пользователя или пароль .";
    }



}
