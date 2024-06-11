using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult ThongKeDoanhThuTheoThang()
        {
            var data = _context.Set<ThongKeDoanhThu>().FromSqlInterpolated($"EXEC ThongKeDoanhThu").ToList();
            return Json(data);
        }
        [HttpGet]
        public IActionResult ThongKeDoanhThuTheoNgay()
        {
            var data = _context.Set<ThongKeDoanhThuTheoNgay>().FromSqlInterpolated($"EXEC ThongKeDoanhThuTheoNgay").ToList();
            return Json(data);
        }
    }
}
