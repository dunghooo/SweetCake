using Microsoft.AspNetCore.Mvc;
using SweetCake.Data;

namespace SweetCake.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TestController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var s = _db.LoaiSP.ToList();
            return View();
        }
    }
}
