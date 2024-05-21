using Microsoft.AspNetCore.Mvc;
using SweetCake.Models;
using System.Diagnostics;

namespace SweetCake.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
			if (HttpContext.Session.GetString("UserName") != null)
			{
				ViewBag.Username = HttpContext.Session.GetString("UserName");
			}
			else
			{
				ViewBag.Username = null;
			}
			return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
