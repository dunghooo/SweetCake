using Microsoft.EntityFrameworkCore;
using SweetCake.Models;

namespace SweetCake.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<SweetCake.Models.SanPham> SanPham { get; set; } = default!;
        public DbSet<SweetCake.Models.LoaiSP> LoaiSP { get; set; } = default!;
        public DbSet<SweetCake.Models.ChiTiet_SP> ChiTiet_SP { get; set; } = default!;
        public DbSet<SweetCake.Models.Anh> Anh { get; set; } = default!;
        public DbSet<SweetCake.Models.ThongTin_NhanHang> ThongTin_NhanHang { get; set; } = default!;
    }
}
