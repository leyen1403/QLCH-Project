using QLCH.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Helpers
{
    public static class Validator
    {
        public static void ValidateNhanVienDTO(NhanVienDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "DTO không được null");

            // Kiểm tra thông tin cá nhân
            if (string.IsNullOrWhiteSpace(dto.MaNV))
                throw new ArgumentException("Mã nhân viên không được để trống");

            if (string.IsNullOrWhiteSpace(dto.HoTen))
                throw new ArgumentException("Họ tên không được để trống");

            if (dto.NgaySinh >= DateTime.Now)
                throw new ArgumentException("Ngày sinh không hợp lệ");

            if (string.IsNullOrWhiteSpace(dto.GioiTinh))
                throw new ArgumentException("Giới tính không được để trống");

            if (string.IsNullOrWhiteSpace(dto.CMND) || dto.CMND.Length < 9)
                throw new ArgumentException("CMND không hợp lệ");

            if (string.IsNullOrWhiteSpace(dto.SoDienThoai) || dto.SoDienThoai.Length < 10)
                throw new ArgumentException("Số điện thoại không hợp lệ");

            if (!IsValidEmail(dto.Email))
                throw new ArgumentException("Email không hợp lệ");

            if (string.IsNullOrWhiteSpace(dto.DiaChi))
                throw new ArgumentException("Địa chỉ không được để trống");

            // Kiểm tra các mã liên quan
            if (dto.MaChucVu <= 0)
                throw new ArgumentException("Mã chức vụ không hợp lệ");

            if (dto.MaPhongBan <= 0)
                throw new ArgumentException("Mã phòng ban không hợp lệ");

            if (dto.MaCuaHang <= 0)
                throw new ArgumentException("Mã cửa hàng không hợp lệ");

            if (string.IsNullOrWhiteSpace(dto.LoaiHopDong))
                throw new ArgumentException("Loại hợp đồng không được để trống");

            if (string.IsNullOrWhiteSpace(dto.TrangThai))
                throw new ArgumentException("Trạng thái không được để trống");

            // Hợp đồng lao động
            if (dto.LuongCoBan.HasValue && dto.LuongCoBan <= 0)
                throw new ArgumentException("Lương cơ bản phải lớn hơn 0");

            if (dto.ThoiHanHD.HasValue && dto.ThoiHanHD <= 0)
                throw new ArgumentException("Thời hạn hợp đồng không hợp lệ");

            if (dto.NgayKy > DateTime.Now)
                throw new ArgumentException("Ngày ký hợp đồng không được sau thời gian hiện tại");

            if (string.IsNullOrWhiteSpace(dto.TrangThaiHopDong))
                throw new ArgumentException("Trạng thái hợp đồng không được để trống");

            // Bảo hiểm
            if (string.IsNullOrWhiteSpace(dto.MaBHXH))
                throw new ArgumentException("Mã BHXH không được để trống");

            if (string.IsNullOrWhiteSpace(dto.MaBHYT))
                throw new ArgumentException("Mã BHYT không được để trống");

            if (string.IsNullOrWhiteSpace(dto.NhaCungCap))
                throw new ArgumentException("Nhà cung cấp không được để trống");

            if (dto.NgayCap > DateTime.Now)
                throw new ArgumentException("Ngày cấp bảo hiểm không hợp lệ");

            if (string.IsNullOrWhiteSpace(dto.TrangThaiBaoHiem))
                throw new ArgumentException("Trạng thái bảo hiểm không được để trống");
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
