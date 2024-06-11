using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SweetCake.Models
{
	[Table("SAN_PHAM")]
    public class SanPham
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]

        [Column(TypeName = "nvarchar(max)")]
        public string Ten { get; set; }
        [Column(TypeName = "ntext")]
        public string? Mota { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string TrangThai { get; set; } = "Đang bán";
        public int LoaiSPId { get; set; }
        public virtual LoaiSP? LoaiSP { get; set; }
        public virtual ICollection<ChiTiet_SP>? ChiTietSPs { get; set; }
        public virtual ICollection<Anh>? Anhs { get; set; }

        
    }
}
