using Microsoft.AspNetCore.Mvc;

namespace Noise.Controllers;

public class InfluenceController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Вплив музики на суспільство";
        ViewData["SectionName"] = "Influence";

        return View();
    }
}