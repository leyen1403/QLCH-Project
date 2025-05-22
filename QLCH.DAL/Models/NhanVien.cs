// NhanVien.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class NhanVien
    {
        public string MaNV { get; set; }

        [Required(ErrorMessage = "Họ tên bắt buộc")]
        public string HoTen { get; set; }

        public DateTime NgaySinh { get; set; }

        [Required]
        [RegularExpression("^(Nam|Nữ)$", ErrorMessage = "Giới tính chỉ được phép là 'Nam' hoặc 'Nữ'")]

        public string GioiTinh { get; set; }

        [Required(ErrorMessage = "CMND/CCCD là bắt buộc")]
        [RegularExpression(@"^\d+$", ErrorMessage = "CMND/CCCD chỉ được chứa chữ số")]
        [MinLength(10, ErrorMessage = "CMND/CCCD phải có ít nhất 10 ký tự")]
        public string CMND_CCCD { get; set; }

        [Required(ErrorMessage = "Mã số thuế là bắt buộc")]
        public string MaSoThue { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Số điện thoại chỉ được chứa chữ số và có từ 10 đến 15 ký tự")]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }

        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Chức vụ là bắt buộc")]
        [Range(1, int.MaxValue, ErrorMessage = "Mã chức vụ phải lớn hơn 0")]
        public int MaChucVu { get; set; }

        [Required(ErrorMessage = "Phòng ban là bắt buộc")]
        [Range(1, int.MaxValue, ErrorMessage = "Mã phòng ban phải lớn hơn 0")]
        public int MaPhongBan { get; set; }

        [Required(ErrorMessage = "Cửa hàng là bắt buộc")]
        [Range(1, int.MaxValue, ErrorMessage = "Mã cửa hàng phải lớn hơn 0")]
        public int MaCuaHang { get; set; }

        [Required(ErrorMessage = "Loại hợp đồng là bắt buộc")]
        [RegularExpression("^(Chính thức|Thử việc|Thời vụ|Bán thời gian)$", ErrorMessage = "Loại hợp đồng không hợp lệ")]
        public string LoaiHopDong { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc")]
        [RegularExpression("^(Đang làm việc|Nghỉ việc|Thử việc)$", ErrorMessage = "Trạng thái không hợp lệ")]
        public string TrangThai { get; set; } = "Đang làm việc";

        public DateTime NgayVaoLam { get; set; }

        public DateTime? NgayNghiViec { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
