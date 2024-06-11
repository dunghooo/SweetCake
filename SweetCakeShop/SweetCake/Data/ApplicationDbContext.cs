using Microsoft.EntityFrameworkCore;

namespace SweetCake.Data
{
	public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<SweetCake.Models.SanPham> SanPham { get; set; } = default!;
		public DbSet<SweetCake.Models.DonHang> DonHang { get; set; } = default!;
		public DbSet<SweetCake.Models.TaiKhoan> TaiKhoan { get; set; } = default!;
		public DbSet<SweetCake.Models.LoaiSP> LoaiSP { get; set; } = default!;
        public DbSet<SweetCake.Models.ChiTiet_SP> ChiTiet_SP { get; set; } = default!;
        public DbSet<SweetCake.Models.Anh> Anh { get; set; } = default!;
		public DbSet<SweetCake.Models.ThongTin_NhanHang> ThongTin_NhanHang { get; set; } = default!;

        public DbSet<ThongKeDoanhThu> ThongKeDoanhThu { get; set; }
        public DbSet<ThongKeDoanhThuTheoNgay> ThongKeDoanhThuTheoNgay { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ThongKeDoanhThu>().HasNoKey();
            modelBuilder.Entity<ThongKeDoanhThuTheoNgay>().HasNoKey();
        }
    }
    public class ThongKeDoanhThu
    {
        public int Thang { get; set; }
        public int Nam { get; set; }
        public int DoanhThu { get; set; }
    }

    public class ThongKeDoanhThuTheoNgay
    {
        public int Thang { get; set; }
        public int DoanhThu { get; set; }
        public int Ngay { get; set; }
    }
}
