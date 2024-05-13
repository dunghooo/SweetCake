using Microsoft.EntityFrameworkCore;
using SweetCake.Models;

namespace SweetCake.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<SweetCake.Models.SanPham> SanPham { get; set; } = default!;
    }
}
