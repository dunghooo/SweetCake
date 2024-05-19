using Microsoft.AspNetCore.Mvc;
using SweetCake.Data;
using SweetCake.Helpers;
using SweetCake.Models;
using Aram.Infrastructure;
using Microsoft.EntityFrameworkCore;

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

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
        [ValidateAntiForgeryToken]
		public IActionResult Create(TaiKhoan obj)
		{
            var user = HttpContext.Session.GetJson<TaiKhoan>("User");
				try
				{
                    if (_context.TaiKhoan.Any(x => x.TenTK == obj.TenTK))
                    {
                        ModelState.AddModelError("TenTK", "Tên tài khoản đã tồn tại");
                    }
                    if (_context.TaiKhoan.Any(x => x.SDT == obj.SDT))
                    {
                        ModelState.AddModelError("SDT", "SDT đã liên kết với tài khoản khác");
                    }
                    if (_context.TaiKhoan.Any(x => x.Email == obj.Email))
                    {
                        ModelState.AddModelError("Email", "Email đã liên kết với tài khoản khác");
                    }
					if (ModelState.IsValid)
					{
						obj.NgayDangKy = DateTime.Now;
						obj.TrangThai = true;
						obj.MatKhau = BCrypt.Net.BCrypt.HashPassword(obj.MatKhau);
						_context.TaiKhoan.Add(obj);
						_context.SaveChanges();
						TempData["Sucess"] = "Thêm tài khoản thành công";
						return RedirectToAction("Index");
					}
					return View(obj);
				}
				catch (Exception)
				{
					TempData["Error"] = "Không thêm mới được tài khoản";
					return RedirectToAction("Index");
				}
		}

        public IActionResult Edit(int? id)
        {
            ViewBag.TaiKhoan = true;
            var user = HttpContext.Session.GetJson<TaiKhoan>("User");
            
                /*if (user.Id == id)
                {
                    TempData["Warning"] = "Tài khoản đăng nhập không được chỉnh sửa";
                    return RedirectToAction("Index");
                }*/
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _context.TaiKhoan.FirstOrDefault(x => x.Id == id);
                if (obj == null)
                {
                    return NotFound();
                }
                if (obj.SDT == "00000000000")
                {
                    TempData["Warning"] = "Tài khoản google không được chỉnh sửa";
                    return RedirectToAction("Index");
                }
                return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaiKhoan obj)
        {
            var user = HttpContext.Session.GetJson<TaiKhoan>("User");
                try
                {
                    var tkNow = _context.TaiKhoan.FirstOrDefault(x => x.Id == obj.Id);
                    if (tkNow == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        if (tkNow.SDT != obj.SDT)
                        {
                            if (_context.TaiKhoan.Any(x => x.SDT == obj.SDT))
                            {
                                ModelState.AddModelError("SDT", "SDT đã liên kết với tài khoản khác");
                            }
                        }
                        if (tkNow.Email != obj.Email)
                        {
                            if (_context.TaiKhoan.Any(x => x.Email == obj.Email))
                            {
                                ModelState.AddModelError("Email", "Email đã liên kết với tài khoản khác");
                            }
                        }
                    }
                    if (ModelState.IsValid)
                    {
                        obj.MatKhau = BCrypt.Net.BCrypt.HashPassword(obj.MatKhau);
                        _context.Entry(tkNow).State = EntityState.Detached;
                        _context.TaiKhoan.Update(obj);
                        _context.SaveChanges();
                        TempData["Sucess"] = "Chỉnh sửa tài khoản thành công";
                        _context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(obj);
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Không sửa được tài khoản";
                    return RedirectToAction("Index");
                }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? txt_ID)
        {
            var user = HttpContext.Session.GetJson<TaiKhoan>("User");
            
                try
                {
                    /*if (user.Id == txt_ID)
                    {
                        TempData["Warning"] = "Tài khoản đăng nhập không được xóa";
                        return RedirectToAction("Index");
                    }*/
                    if (txt_ID == null || txt_ID == 0)
                    {
                        return NotFound();
                    }
                    var obj = _context.TaiKhoan.FirstOrDefault(x => x.Id == txt_ID);
                    if (obj == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        obj.TrangThai = false;
                        _context.SaveChanges();
                        TempData["Sucess"] = "Xóa tài khoản thành công";

                        _context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    
                    TempData["Error"] = "Lỗi nghiêm trọng hãy báo IT để được hỗ trợ";
                    return RedirectToAction("Index");
                }

        }

        public IActionResult Search(string Key)
        {
            ViewBag.TaiKhoan = true;
            if (Key != null)
            {
                int total = _context.TaiKhoan.Where(x => x.TenTK.ToLower().Contains(Key.ToLower()) && x.TrangThai == true).Count();
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
                ViewBag.Search = Key;
                if (total > 0)
                {
                    var result = _context.TaiKhoan.Where(x => x.TenTK.ToLower().Contains(Key.ToLower()) && x.TrangThai == true).Skip((currentpage - 1) * ITEM_PER_PAGE).Take(ITEM_PER_PAGE).ToList();
                    return View("Index", result);
                }
                else
                {
                    return View("Index", null);
                }
            }
            else
            {
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
                ViewBag.Search = Key;
                if (total > 0)
                {
                    var result = _context.TaiKhoan.Where(x => x.TrangThai == true).Skip((currentpage - 1) * ITEM_PER_PAGE).Take(ITEM_PER_PAGE).ToList();
                    return View("Index", result);
                }
                else
                {
                    return View("Index", null);
                }
            }
        }
    }

}
