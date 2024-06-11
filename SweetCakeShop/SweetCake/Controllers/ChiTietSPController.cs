using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SweetCake.Data;
using SweetCake.Models;

namespace SweetCake.Controllers
{
	public class ChiTietSPController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChiTietSPController(ApplicationDbContext context)
        {
            // Khai báo constructor
            _context = context;
        }
        public IActionResult Index(int? SanPhamId)
        {
            var detail = _context.ChiTiet_SP.Where(x => x.SanPhamId == SanPhamId).ToList();
            ViewBag.SanPhamId = SanPhamId;
            return View(detail);
        }

        public IActionResult Create(int? SanPhamId)
        {
            var sp = _context.SanPham.FirstOrDefault(x => x.Id == SanPhamId);
            ViewBag.tenSP = sp.Ten;
            ViewBag.SanPhamId = sp.Id;
            return View();
        }

        [HttpPost]
        public IActionResult Create(ChiTiet_SP ct, int SanPhamId)
        {
            try
            {
                if (ct.NgaySanXuat >= ct.HanSuDung)
                {
                    ModelState.AddModelError("NgaySanXuat", "Ngày sản suất phải bé hơn hạn sử dụng");
                }
                ct.SanPhamId = SanPhamId;
                var sp = _context.SanPham.FirstOrDefault(x => x.Id == SanPhamId);
                ViewBag.tenSP = sp.Ten;
                if (ModelState.IsValid)
                {
                    _context.Add(ct);
                    _context.SaveChanges();
                    TempData["Sucess"] = "Thêm chi tiết sản phẩm thành công!!";
                }
                ViewBag.SanPhamId = SanPhamId;
                return View(ct);
            }
            catch (Exception)
            {

                TempData["Error"] = "Lỗi nghiêm trọng hãy báo IT để được hỗ trợ";
                return RedirectToAction("Index", "SanPham");
            }

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var SanPhamID = _context.ChiTiet_SP.SingleOrDefault(x => x.Id == id);
            var SP = _context.SanPham.FirstOrDefault(X => X.Id == SanPhamID.SanPhamId);
            ViewBag.tenSP = SP.Ten;
            if (SanPhamID == null)
            {
                return NotFound();
            }

            return View(SanPhamID);
        }

        [HttpPost]
        public IActionResult Edit(ChiTiet_SP ct, int id)
        {
            try
            {
                var ChiTiet = _context.ChiTiet_SP.SingleOrDefault(x => x.Id == id);
                var SP = _context.SanPham.FirstOrDefault(X => X.Id == ChiTiet.SanPhamId);
                _context.Entry(ChiTiet).State = EntityState.Detached;
                _context.Entry(SP).State = EntityState.Detached;
                ViewBag.tenSP = SP.Ten;
                ct.SanPhamId = SP.Id;
                if (ModelState.IsValid)
                {
                    _context.Update(ct);
                    _context.SaveChanges();
                    TempData["Sucess"] = "Sửa Chi tiết sản phẩm thành công!!";
                    string returnUrl = Url.Action("Index", "ChiTietSP", new { SanPhamId = ct.SanPhamId });
                    return Redirect(returnUrl);
                }
                return View(ct);
            }
            catch (Exception)
            {
                TempData["Error"] = "Lỗi nghiêm trọng hãy báo IT để được hỗ trợ";
                string returnUrl = Url.Action("Index", "ChiTietSP", new { SanPhamId = ct.SanPhamId });
                return Redirect(returnUrl);
            }
        }

        public IActionResult Delete(int? txt_ID)
        {
            try
            {
                if (txt_ID == null || txt_ID == 0)
                {
                    return NotFound();
                }
                var obj = _context.ChiTiet_SP.FirstOrDefault(x => x.Id == txt_ID);
                if (obj == null)
                {
                    return NotFound();
                }
                else
                {
                    obj.TrangThai = false;
                    _context.SaveChanges();
                    TempData["Sucess"] = "Xóa chi tiết sản phẩm thành công";
                    return RedirectToAction("Index", "SanPham");
                }
            }
            catch (Exception)
            {
                TempData["Error"] = "Lỗi nghiêm trọng hãy báo IT để được hỗ trợ";
                return RedirectToAction("Index", "SanPham");
            }
        }
    }
}
