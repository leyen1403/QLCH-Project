// HopDongLaoDong.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class HopDongLaoDong
    {
        [Range(1, int.MaxValue, ErrorMessage = "Mã hợp đồng không hợp lệ")]
        public int MaHopDong { get; set; }

        [Required(ErrorMessage = "Mã nhân viên là bắt buộc")]
        public string MaNV { get; set; }

        [Required(ErrorMessage = "Loại hợp đồng là bắt buộc")]
        public string LoaiHopDong { get; set; }

        public DateTime NgayKy { get; set; }
        public DateTime NgayHieuLuc { get; set; }
        public DateTime? NgayKetThuc { get; set; }

        [Required(ErrorMessage = "Lương cơ bản là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Lương cơ bản phải lớn hơn hoặc bằng 0")]
        public decimal LuongCoBan { get; set; }

        [Required(ErrorMessage = "Thời hạn hợp đồng là bắt buộc")]
        public int ThoiHanHD { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc")]
        [RegularExpression("^(Còn hiệu lực|Hết hạn|Tạm dừng)$", ErrorMessage = "Trạng thái không hợp lệ")]
        public string TrangThai { get; set; }
    }
}
