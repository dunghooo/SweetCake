using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SweetCake.Models
{
	[Table("DONHANG_CHITIET")]
	public class DonHang_ChiTiet
	{
		[Key]
		public int Id { get; set; }
		public int SoLuong { get; set; }
		public int DonHangId { get; set; }
		public virtual DonHang DonHang { get; set; }
		public int ChiTiet_SPId { get; set; }
		public virtual ChiTiet_SP? ChiTiet_SP { get; set; }
	}
}
