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
    }
}
