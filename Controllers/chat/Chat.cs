using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SpeedDeal.Controllers.chat;

public class Chat : Controller
{
    // GET
    public IActionResult Index()
    {
        ViewBag.test = "htllo mazafaka";
        return View();
    }
}