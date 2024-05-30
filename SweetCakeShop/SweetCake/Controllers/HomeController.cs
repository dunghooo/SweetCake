using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SweetCake.Data;
using SweetCake.Models;
using System.Diagnostics;

namespace SweetCake.Controllers
{
    public class HomeController : Controller
    {
		private readonly ApplicationDbContext _db;

		public HomeController(ApplicationDbContext db)
		{
			_db = db;
		}
		public const int ITEM_PER_PAGE = 6;

		[BindProperty(SupportsGet = true, Name = "p")]
		public int currentpage { get; set; }

		public int countpages { get; set; }


		public IActionResult Index()
        {
			var splist = _db.SanPham.Where(x => (x.TrangThai == "Đang bán" || x.TrangThai == "Sale")).Include(x => x.ChiTietSPs).Include(x => x.Anhs).Take(3).ToList();
			if (HttpContext.Session.GetString("UserName") != null)
			{
				ViewBag.Username = HttpContext.Session.GetString("UserName");
			}
			else
			{
				ViewBag.Username = null;
			}
			return View(splist);
        }

        public IActionResult About()
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

		public IActionResult GetImage(int? id_sp, int? id_hinh)
		{
			if (id_hinh != null)
			{
				var hinh = _db.Anh
				.FirstOrDefault(x => x.Id == id_hinh);
				var imageBytes = System.IO.File.ReadAllBytes("wwwroot/img/products/" + hinh.TenAnh); // Đọc file ảnh thành mảng byte
				return File(imageBytes, "image/jpeg");
			}
			else if (id_sp != null)
			{
				var hinh = _db.Anh
					.FirstOrDefault(x => x.SanphamId == id_sp);
				if (hinh != null)
				{
					var imageBytes = System.IO.File.ReadAllBytes("wwwroot/img/products/" + hinh.TenAnh);
					// Đọc file ảnh thành mảng byte
					return File(imageBytes, "image/jpeg");
				}
				else
				{
					return NotFound();
				}

			}
			else
			{
				return NotFound();
			}
		}

		public IActionResult Product_Detail(int id_sp)
		{
            if (HttpContext.Session.GetString("UserName") != null)
            {
                ViewBag.Username = HttpContext.Session.GetString("UserName");
            }
            var detail = _db.SanPham.Include(x => x.ChiTietSPs).Include(x => x.Anhs).FirstOrDefault(x => x.Id == id_sp);
			var relatedProduct = _db.SanPham.Where(x => (x.LoaiSPId == detail.LoaiSPId) && (x.TrangThai == "Đang bán" || x.TrangThai == "Sale"))
								.Include(x => x.ChiTietSPs)
								.Include(x => x.Anhs)
								.ToList();
			if (relatedProduct.Count() > 3)
			{
				relatedProduct = relatedProduct.Take(3).ToList();
			}
			ViewData["RelateProduct"] = relatedProduct;
			return View(detail);
		}

		public IActionResult Shop(int? id)
		{
            if (HttpContext.Session.GetString("UserName") != null)
            {
                ViewBag.Username = HttpContext.Session.GetString("UserName");
            }
            ViewBag.SPID = id;
			ViewBag.LoaiSP = _db.LoaiSP.Where(x => x.TrangThai == true).OrderBy(x => x.Id).ToList();
			int total;
			if(id == null)
			{
				total = _db.SanPham.Where(x => (x.TrangThai == "Đang bán" || x.TrangThai == "Sale")).Count();
			}
            else
            {
				total = _db.SanPham.Where(x => (x.TrangThai == "Đang bán" || x.TrangThai == "Sale") && x.LoaiSPId == id).Count();
			}
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
			if(total > 0)
			{
				if(id == null)
				{
					var result = _db.SanPham.Where(x => x.TrangThai == "Đang bán" || x.TrangThai == "Sale").Include(x => x.ChiTietSPs).Include(x => x.Anhs).Skip((currentpage - 1) * ITEM_PER_PAGE).Take(ITEM_PER_PAGE).ToList();
					ViewBag.ProductList = true;
					return View(result);
				}
				else
				{
					var result = _db.SanPham.Where(x => (x.TrangThai == "Đang bán" || x.TrangThai == "Sale") && x.LoaiSPId == id).Include(x => x.ChiTietSPs).Include(x => x.Anhs).Skip((currentpage - 1) * ITEM_PER_PAGE).Take(ITEM_PER_PAGE).ToList();
					ViewBag.ProductList = true;
					return View(result);
				}

			}
			else
			{
				ViewBag.ProductList = true;
				return View(null);
			}
		}

		public IActionResult CheckOut()
		{
			return View();
		}
	}
}
