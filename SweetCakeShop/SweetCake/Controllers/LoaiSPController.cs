using Microsoft.AspNetCore.Mvc;
using SweetCake.Data;
using SweetCake.Models;

namespace SweetCake.Controllers
{
	public class LoaiSPController : Controller
    {

        private readonly ApplicationDbContext _context;
        public LoaiSPController(ApplicationDbContext context)
        {
            _context = context;
        }
        public const int ITEM_PER_PAGE = 5;

        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentpage { get; set; }

        public int countpages { get; set; }
        public IActionResult Index()
        {
            int total = _context.LoaiSP.Count();
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
                var result = _context.LoaiSP.Where(x => x.TrangThai == true).Skip((currentpage - 1) * ITEM_PER_PAGE).Take(ITEM_PER_PAGE).ToList();
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
        public IActionResult Create(LoaiSP Loai)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Loai.TrangThai = true;
                    _context.Add(Loai);
                    _context.SaveChanges();
                    TempData["Sucess"] = "Thêm loại sản phẩm thành công!!";
                    return RedirectToAction("Index");
                }
                return View(Loai);
            }
            catch (Exception)
            {
                TempData["Error"] = "Lỗi không tạo mới được chi tiết sản phẩm";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.LoaiSP = true;
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var loaifromdb = _context.LoaiSP.FirstOrDefault(x => x.Id == id);
            if (loaifromdb == null)
            {
                return NotFound();
            }
            return View(loaifromdb);

        }

        [HttpPost]

        public IActionResult Edit(LoaiSP loai)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Update(loai);
                    _context.SaveChanges();
                    TempData["Sucess"] = "Sửa loại sản phẩm thành công!!";
                    return RedirectToAction("Index");
                }
                return View(loai);
            }
            catch (Exception)
            {
                TempData["Error"] = "Lỗi cập nhật sản phẩm không thành công!!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Delete(int? txt_ID)
        {
            try
            {

                if (txt_ID == null || txt_ID == 0)
                {
                    return NotFound();
                }
                var obj = _context.LoaiSP.FirstOrDefault(x => x.Id == txt_ID);
                if (obj == null)
                {
                    return NotFound();
                }
                else
                {
                    obj.TrangThai = false;
                    _context.SaveChanges();
                    TempData["Sucess"] = "Xóa loại sản phẩm thành công";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                TempData["Error"] = "Lỗi xóa sản phẩm không thành công!!";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Search(string Key)
        {
            ViewBag.LoaiSP = true;
            if (Key != null)
            {
                int total = _context.LoaiSP.Where(x => (x.TrangThai == true && x.Ten.ToLower().Contains(Key.ToLower()))).Count();
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
                    var result = _context.LoaiSP.Where(x => (x.TrangThai == true && x.Ten.ToLower().Contains(Key.ToLower()))).Skip((currentpage - 1) * ITEM_PER_PAGE).Take(ITEM_PER_PAGE).ToList();
                    return View("Index", result);
                }
                else
                {
                    return View("Index", null);
                }
            }
            else
            {
                int total = _context.LoaiSP.Where(x => x.TrangThai == true).Count();
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
                    var result = _context.LoaiSP.Where(x => x.TrangThai == true).Skip((currentpage - 1) * ITEM_PER_PAGE).Take(ITEM_PER_PAGE).ToList();
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
