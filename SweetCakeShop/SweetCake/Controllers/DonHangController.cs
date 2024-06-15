using Aram.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SweetCake.Data;
using SweetCake.Models;

namespace SweetCake.Controllers
{
    public class DonHangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonHangController(ApplicationDbContext context)
        {
            _context = context;
        }
        public const int ITEM_PER_PAGE = 5;

        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentpage { get; set; }

        public int countpages { get; set; }
        public IActionResult Index(string? trangThai)
        {
            if (trangThai == null)
            {
                trangThai = "cho duyet";
                ViewBag.TrangThai = trangThai;
            }
            else
            {
                ViewBag.TrangThai = trangThai;
            }
            int total = _context.DonHang
                .Where(x => x.TrangThaiDonHang == trangThai)
                .Count();
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
            ViewBag.dh = _context.DonHang.ToList();
            if (total > 0)
            {
                var result = _context.DonHang
                .Where(x => x.TrangThaiDonHang == trangThai)
                .Include(x => x.TaiKhoan)
                .Include(x => x.ThongTin_NhanHang)
                .Include(x => x.DonHang_ChiTiets)
                .ThenInclude(x => x.ChiTiet_SP)
                .ThenInclude(x => x.SanPham)
                .ThenInclude(x => x.LoaiSP)
                .OrderByDescending(x => x.ThoiGianHuy == null ? x.ThoiGianTao : x.ThoiGianHuy)
                .Skip((currentpage - 1) * ITEM_PER_PAGE).Take(ITEM_PER_PAGE)
                .ToList();
                int count = _context.DonHang.Where(x => x.TrangThaiDonHang == "cho duyet").Count();
                ViewBag.Count = count;
                return View(result);
            }
            else
            {
                return View(null);
            }
        }

        public IActionResult DuyetDon(int id, string? returnUrl)
        {
            var user = HttpContext.Session.GetJson<TaiKhoan>("User");
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                else
                {
                    var DonHang = _context.DonHang.FirstOrDefault(a => a.Id == id);
                    if (DonHang.TrangThaiDonHang == "cho duyet")
                    {
                        DonHang.TrangThaiDonHang = "dang giao";
                        DonHang.ThoiGianHuy = DateTime.Now;
                    }
                    else
                    {
                        DonHang.TrangThaiDonHang = "da giao";
                        DonHang.ThoiGianHuy = DateTime.Now;
                    }
                    _context.Update(DonHang);
                    _context.SaveChanges();
                    TempData["Sucess"] = "Thao tác thành công";
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "DonHang");
                    }
                }
            }
            catch (Exception)
            {

                TempData["Error"] = "Lỗi!! Không thao tác được đơn hàng này!!";
                return RedirectToAction("Index", "DonHang");
            }
            
        }

        public IActionResult GiaoHangThanhCong(int id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                else
                {
                    var DonHang = _context.DonHang.FirstOrDefault(a => a.Id == id);
                    DonHang.TrangThaiDonHang = "da giao";
                    _context.Update(DonHang);
                    _context.SaveChanges();
                    TempData["Sucess"] = "Thao tác thành công";
                    return RedirectToAction("Index", "DonHang");
                }
            }
            catch (Exception)
            {

                TempData["Error"] = "Lỗi!! Không thao tác được đơn hàng này!!";
                return RedirectToAction("Index", "DonHang");
            }
            
        }

        public IActionResult HuyDon(int txt_ID, string? returnUrl)
        {
            var user = HttpContext.Session.GetJson<TaiKhoan>("User");
            try
            {
                if (txt_ID == null)
                {
                    return NotFound();
                }
                else
                {
                    var DonHang = _context.DonHang
                            .Include(x => x.DonHang_ChiTiets)
                            .FirstOrDefault(a => a.Id == txt_ID);
                    DonHang.TrangThaiDonHang = "da huy";
                    DonHang.ThoiGianHuy = DateTime.Now;
                    foreach (var item in DonHang.DonHang_ChiTiets)
                    {
                        var sp = _context.ChiTiet_SP.FirstOrDefault(a => a.Id == item.ChiTiet_SPId);
                        sp.SoLuong += item.SoLuong;
                        _context.Update(sp);
                        _context.SaveChanges();
                    }
                    _context.Update(DonHang);
                    _context.SaveChanges();
                    TempData["Sucess"] = "Hủy đơn thành công";
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "DonHang");
                    }
                }
            }
            catch (Exception)
            {

                TempData["Error"] = "Không hủy được đơn hàng!!";
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "DonHang");
                }
            }
        }


    }
}
