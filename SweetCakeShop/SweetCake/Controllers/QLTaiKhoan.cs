using Microsoft.AspNetCore.Mvc;
using SweetCake.Data;
using SweetCake.Helpers;

namespace SweetCake.Controllers
{
    public class QLTaiKhoan : Controller
    {
        private readonly ApplicationDbContext _context;
        public QLTaiKhoan(ApplicationDbContext context)
        {
            _context = context;
        }
		public const int ITEM_PER_PAGE = 5;

		[BindProperty(SupportsGet = true, Name = "p")]
		public int currentpage { get; set; }

		public int countpages { get; set; }
		public IActionResult Index()
        {
            ViewBag.TaiKhoan = true;
            int total = _context.TaiKhoan.Where(x => x.TrangThai == true).Count();
            countpages = (int)Math.Ceiling((double)total / ITEM_PER_PAGE);
			if (currentpage < 1)
			{
				currentpage = 1;
			}
			if (currentpage > countpages)
			{
				currentpage = countpages;
			}
			ViewBag.CurrentPage = currentpage;
			ViewBag.CountPages = countpages;
			if (total > 0)
			{
				var result = _context.TaiKhoan.Where(x => x.TrangThai == true).Skip((currentpage - 1) * ITEM_PER_PAGE).Take(ITEM_PER_PAGE).ToList();
				return View(result);
			}
			else
			{
				return View(null);
			}
		}

    }
}
