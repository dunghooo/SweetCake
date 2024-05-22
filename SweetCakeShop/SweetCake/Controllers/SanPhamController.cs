using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SweetCake.Data;
using SweetCake.Models;
using System.Text;

namespace SweetCake.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webhost;

        public SanPhamController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _webhost = webHostEnvironment;
            _db = db;
        }

        public const int ITEM_PER_PAGE = 5;

        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentpage { get; set; }

        public int countpages { get; set; }

        public IActionResult Index()
        {
            int total = _db.SanPham.Include(x => x.Anhs).Where(x => (x.TrangThai == "Đang bán" || x.TrangThai == "Sale")).Count();
            countpages = (int)Math.Ceiling((double)total / ITEM_PER_PAGE);

            if(currentpage < 1)
            {
                currentpage = 1;
            }

            if(currentpage < countpages)
            {
                currentpage = countpages;
            }

            ViewBag.CurrentPage = currentpage;
            ViewBag.CountPages = countpages;

            if (total > 0)
            {
                var result = _db.SanPham.Include(x => x.Anhs).Where(x => (x.TrangThai == "Đang bán" || x.TrangThai == "Sale")).Skip((currentpage -1) * ITEM_PER_PAGE).Take(ITEM_PER_PAGE).ToList(); 
                 return View(result);
            }
            else
            {
                return View(null);
            }
        }

        public IActionResult Create()
        {
            var loaiSPList = _db.LoaiSP.Where(x => x.TrangThai == true).OrderBy(x => x.Id)
                                     .Select(x => new SelectListItem
                                     {
                                         Value = x.Id.ToString(),
                                         Text = x.Ten
                                     })
                                     .ToList();
            ViewBag.LoaiSPid = new SelectList(loaiSPList, "Value", "Text");
            List<string> listTrangThai = new List<string> { "Đang bán", "Ngừng bán", "Sale" };
            ViewBag.TrangThai = new SelectList(listTrangThai);
            return View();
        }
        [HttpPost]
        public IActionResult Create(SanPham sp, IFormFile[] files, int gia, int soluong, DateTime ngaysanxuat, DateTime hansudung)
        {
            try
            {
                var loaiSPList = _db.LoaiSP.Where(x => x.TrangThai == true).OrderBy(x => x.Id)
                                     .Select(x => new SelectListItem
                                     {
                                         Value = x.Id.ToString(),
                                         Text = x.Ten
                                     })
                                     .ToList();
                ViewBag.LoaiSPid = new SelectList(loaiSPList, "Value", "Text");
                List<string> listTrangThai = new List<string> { "Đang bán", "Ngừng bán", "Sale" };
                ViewBag.TrangThai = new SelectList(listTrangThai);

                if (gia <= 0)
                {
                    ViewBag.ktGia = "Giá không hợp lệ";

                }

                if (soluong <= 0)
                {
                    ViewBag.ktSoLuong = "Số lượng không hợp lệ";

                }

                if (ngaysanxuat >= hansudung)
                {
                    ViewBag.ktNgay = "Ngày sản suất phải bé hơn hạn sử dụng";

                }
                if (ngaysanxuat == DateTime.MinValue && hansudung == DateTime.MinValue)
                {
                    ViewBag.ktTrongNgay = "Vui lòng nhập ngày sản suất, hạn sử dụng";

                }

                if (ViewBag.ktGia != null || ViewBag.ktSoLuong != null || ViewBag.ktKichThuoc != null || ViewBag.ktNgay != null || ViewBag.ktTrongNgay != null)
                {
                    return View(sp);
                }

                if (ModelState.IsValid)
                {
                    _db.Add(sp);
                    _db.SaveChanges();
                    var SP_CT = new ChiTiet_SP();
                    SP_CT.SanPhamId = sp.Id;
                    SP_CT.Gia = gia;
                    SP_CT.SoLuong = soluong;
                    SP_CT.NgaySanXuat = ngaysanxuat;
                    SP_CT.HanSuDung = hansudung;
                    _db.Add(SP_CT);
                    _db.SaveChanges();
                    foreach (var item in files)
                    {
                        Uploadfile(item);
                        Anh anh = new Anh();
                        anh.SanphamId = sp.Id;
                        if (_db.Anh.Any(a => a.TenAnh == item.FileName))
                        {
                            anh.TenAnh = randomimagename(5) + item.FileName;
                        }
                        anh.TenAnh = item.FileName;

                    }
                    _db.SaveChanges();
                    TempData["Sucess"] = "Thêm sản phẩm thành công!!";
                    return RedirectToAction("Index");

                }
                return View(sp);
            }
            catch (Exception)
            {
                TempData["Error"] = "Không thêm mới được sản phẩm";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(int? id)
        {
            var loaiSPList = _db.LoaiSP.Where(x => x.TrangThai == true).OrderBy(x => x.Id)
                                     .Select(x => new SelectListItem
                                     {
                                         Value = x.Id.ToString(),
                                         Text = x.Ten
                                     })
                                     .ToList();
            ViewBag.LoaiSPid = new SelectList(loaiSPList, "Value", "Text");
            List<string> listTrangThai = new List<string> { "Đang bán", "Ngừng bán", "Sale" };
            ViewBag.TrangThai = new SelectList(listTrangThai);
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var SanPhamID = _db.SanPham.FirstOrDefault(x => x.Id == id);
            if(SanPhamID == null) 
            {
                return NotFound(); 
            }
            var danhSachAnh = _db.Anh.Where(x => x.SanphamId == id).ToList();
            ViewBag.DanhSachAnh = danhSachAnh;
            return View(SanPhamID);
        }

        [HttpPost]
        public IActionResult Edit(SanPham obj, IFormFile[] files)
        {
            var loaiSPList = _db.LoaiSP.Where(x => x.TrangThai == true).OrderBy(x => x.Id)
                                     .Select(x => new SelectListItem
                                     {
                                         Value = x.Id.ToString(),
                                         Text = x.Ten
                                     })
                                     .ToList();
            ViewBag.LoaiSPid = new SelectList(loaiSPList, "Value", "Text");
            List<string> listTrangThai = new List<string> { "Đang bán", "Ngừng bán", "Sale" };
            ViewBag.TrangThai = new SelectList(listTrangThai);
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Update(obj);
                    _db.SaveChanges();
                    var anhdaco = _db.Anh.Where(x => x.SanphamId == obj.Id).ToList();
                    if(files.Length > 0)
                    {
                        foreach (var anh in anhdaco)
                        {
                            _db.Remove(anh);
                            _db.SaveChanges();
                        }
                        foreach(var item in files)
                        {
                            Uploadfile(item);
                            Anh anh = new Anh();
                            anh.SanphamId = obj.Id;
                            _db.Anh.Add(anh);
                            if (_db.Anh.Any(a => a.TenAnh == item.FileName))
                            {
                                anh.TenAnh = randomimagename(5) + item.FileName;
                            }
                            anh.TenAnh = item.FileName;

                        }
                        _db.SaveChanges();
                        TempData["Sucess"] = "Sửa sản phẩm thành công!!";
                        return RedirectToAction("Index");
                    }
                    
                }
                return View(obj);
            }
            catch (Exception)
            {

                TempData["Error"] = "Không sửa được sản phẩm";
                return RedirectToAction("Index");
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
                var obj = _db.SanPham.FirstOrDefault(x => x.Id == txt_ID);
                if (obj == null)
                {
                    return NotFound();
                }
                else
                {
                    obj.TrangThai = "Ngừng Bán";
                    _db.SaveChanges();
                    TempData["Sucess"] = "Xóa sản phẩm thành công";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                TempData["Error"] = "Lỗi nghiêm trọng hãy báo IT để được hỗ trợ";
                return RedirectToAction("Index");
            }
        }

        static string randomimagename(int quantity)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+-=[]{}|;:,.<>?";
            StringBuilder password = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < quantity; i++)
            {
                int index = random.Next(chars.Length);
                password.Append(chars[index]);
            }

            return password.ToString();
        }

        public IActionResult Search(string Key)
        {
            if(Key != null)
            {
                int total = _db.SanPham.Include(x => x.Anhs).Where(x => (x.TrangThai == "Đang Bán" || x.TrangThai == "Sale") && (x.Id.ToString() == Key || x.Ten.Contains(Key))).Count();
                int countpages = (int)Math.Ceiling((double)total / ITEM_PER_PAGE);

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
                if(total > 0)
                {
                    var result = _db.SanPham.Include(x => x.Anhs).Where(x => (x.TrangThai == "Đang bán" || x.TrangThai == "Mới") && (x.Id.ToString() == Key || x.Ten.Contains(Key))).Skip((currentpage - 1) * ITEM_PER_PAGE).Take(ITEM_PER_PAGE).ToList();
                    return View("Index", result);
                }
                else
                {
                    return View("Index", null);
                }
            }
            else
            {
                int total = _db.SanPham.Include(x => x.Anhs).Where(x => (x.TrangThai == "Đang Bán" || x.TrangThai == "Sale")).Count();
                int countpages = (int)Math.Ceiling((double)total / ITEM_PER_PAGE);

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
                if(total > 0)
                {
                    var result = _db.SanPham.Include(x => x.Anhs).Where(x => (x.TrangThai == "Đang bán" || x.TrangThai == "Sale")).Skip((currentpage - 1) * ITEM_PER_PAGE).Take(ITEM_PER_PAGE).ToList();
                    return View("Index", result);
                }
                else
                {
                    return View("Index", null);
                }

            }
            
        }

        public void Uploadfile(IFormFile file)
        {
            if (file != null)
            {
                string uploadDir = Path.Combine(_webhost.WebRootPath, "img/products"); // đưa ảnh vào file
                string filePath = Path.Combine(uploadDir, file.FileName); // đưa ảnh vào file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
        }

    }
}
