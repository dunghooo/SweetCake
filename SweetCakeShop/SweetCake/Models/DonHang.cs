using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SweetCake.Models
{
	[Table("DON_HANG")]
	public class DonHang
	{
		[Key]
		public int Id { get; set; }
		public DateTime? ThoiGianTao { get; set; } = DateTime.Now;
		public DateTime? ThoiGianHuy { get; set; }
		public bool? TrangThaiThanhToan { get; set; } = false;
		public string? TrangThaiDonHang { get; set; } = "cho duyet";
		public int TaiKhoanId { get; set; }
		public virtual TaiKhoan TaiKhoan { get; set; }
		public ICollection<DonHang_ChiTiet>? DonHang_ChiTiets { get; set; }
		public string PaymentMethod { get; set; }
		public ThongTin_NhanHang? ThongTin_NhanHang { get; set; }
	}
}
