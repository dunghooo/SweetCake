using Aram.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SweetCake.Data;
using SweetCake.Models;

namespace SweetCake.Controllers
{
    public class DangNhapDangKy : Controller
    {
        private readonly ApplicationDbContext _db;

        public DangNhapDangKy (ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            if (returnUrl != null)
            {
                ViewBag.returnUrl = returnUrl;
            }
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public IActionResult Login(TaiKhoan tk, string? returnUrl)
        {
            var user = _db.TaiKhoan.FirstOrDefault(x => x.TenTK == tk.TenTK);

            if (user != null)
            {
                if (tk.MatKhau == null)
                {
                    ViewBag.ErrPassword = "Mật khẩu không được để trống";
                    return View(tk);
                }
                if (BCrypt.Net.BCrypt.Verify(tk.MatKhau, user.MatKhau))
                {
                    var currentDate = DateTime.Now;

                    
                    HttpContext.Session.SetString("UserName", user.TenTK.ToString());
                    HttpContext.Session.SetJson("User", user);
                    if (user.TrangThai == false)
                    {
                        TempData["warning"] = "Tài khoản của bạn đã bị khóa. Vui lòng liên hệ admin website để được hỗ trợ";
                        return View(tk);
                    }
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    if (user.LoaiTK)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {

                    TempData["Warning"] = "Tên tài khoản và mật khẩu không chính xác, vui lòng nhập lại";
                    return View(tk);
                }
            }
            ViewBag.ErrPassword = "Tên tài khoản hoặc mật khẩu không chính xác";
            return View(tk);
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(TaiKhoan tk)
        {
            if (ModelState.IsValid)
            {

                var existingAccount = _db.TaiKhoan.FirstOrDefault(t => t.TenTK == tk.TenTK);
                var existingEmail = _db.TaiKhoan.FirstOrDefault(t => t.Email == tk.Email);
                var existingPhone = _db.TaiKhoan.FirstOrDefault(t => t.SDT == tk.SDT);


                if (existingAccount != null)
                {
                    TempData["errorMessage"] = "Tài khoản đã tồn tại!";
                    return View();
                }
                else if (existingEmail != null)
                {
                    TempData["errorMessage"] = "Email đã được sử dụng!";
                    return View();
                }
                else if (existingPhone != null)
                {
                    TempData["errorMessage"] = "Số điện thoại đã được sử dụng!";
                    return View();
                }
                else
                {
                    var db = new TaiKhoan()
                    {
                        TenTK = tk.TenTK,
                        Email = tk.Email,
                        MatKhau = BCrypt.Net.BCrypt.HashPassword(tk.MatKhau),
                        SDT = tk.SDT,
                        DiaChi = tk.DiaChi,
                        NgayDangKy = DateTime.Now,
                        TrangThai = true,
                    };
                    _db.TaiKhoan.Add(db);
                    _db.SaveChanges();
                    TempData["Sucess"] = "Đăng kí thành công!";
                    return RedirectToAction("Login");
                }
            }
            else
            {
                TempData["Error"] = "Đăng ký thất bại!";
                return View();
            }
        }

		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home");
		}
	}
}
