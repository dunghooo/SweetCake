using Microsoft.AspNetCore.Mvc;
using SweetCake.Data;

namespace SweetCake.Controllers
{
	public class AdminController : Controller
	{
		private readonly ApplicationDbContext _context;
		public AdminController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
