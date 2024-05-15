using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SweetCake.Models
{
    [Table("THONGTIN_NHANHANG")]
    public class ThongTin_NhanHang
    {
        [Key]
        public int Id { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public string? GhiChu { get; set; }
        [ForeignKey("SanPham")]
        public int SanPhamId { get; set; }

    }
}
