﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SweetCake.Models
{
	[Table("CHI_TIET_SP")]
    public class ChiTiet_SP
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Giá không được để trống")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Gía không được chứa ký tự đặc biệt")]
        [Range(10000, 100000000, ErrorMessage = "Gía từ 10.000 đến 10.000.000")]
        
        public int Gia { get; set; }
        [Required(ErrorMessage = "Số lượng không được để trống")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Số lượng không được chứa ký tự đặc biệt")]
        [Range(1, 1000, ErrorMessage = "Số lượng phải từ 1 đến 1000")]
        public int SoLuong { get; set; }
        [Required(ErrorMessage = "Ngày sản xuất không được để trống")]
        public DateTime NgaySanXuat { get; set; }
        [Required(ErrorMessage = "Hạn sử dụng không được để trống")]
        public DateTime HanSuDung { get; set; }
        public int SanPhamId { get; set; }
        public SanPham? SanPham { get; set; }
        public bool TrangThai { get; set; } = true;
    }
}
