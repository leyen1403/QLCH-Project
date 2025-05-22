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
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.MaNV)) errors.Add("Mã nhân viên không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.HoTen)) errors.Add("Họ tên không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.CMND) || dto.CMND.Length < 9) errors.Add("CMND không hợp lệ.");
            if (string.IsNullOrWhiteSpace(dto.SoDienThoai) || dto.SoDienThoai.Length != 10) errors.Add("Số điện thoại phải có 10 chữ số.");
            if (string.IsNullOrWhiteSpace(dto.Email) || !dto.Email.Contains("@")) errors.Add("Email không hợp lệ.");
            if ((DateTime.Now - dto.NgaySinh).TotalDays < 18 * 365) errors.Add("Nhân viên phải từ 18 tuổi trở lên.");

            if (dto.MaPhongBan <= 0) errors.Add("Phòng ban không hợp lệ.");
            if (dto.MaChucVu <= 0) errors.Add("Chức vụ không hợp lệ.");
            if (dto.MaCuaHang <= 0) errors.Add("Cửa hàng không hợp lệ.");

            if (string.IsNullOrWhiteSpace(dto.LoaiHopDong)) errors.Add("Loại hợp đồng không được để trống.");
            if (dto.NgayKetThuc < dto.NgayBatDau) errors.Add("Ngày kết thúc phải sau ngày bắt đầu hợp đồng.");

            if (!string.IsNullOrEmpty(dto.MaBHXH) && dto.MaBHXH.Length < 5)
                errors.Add("Mã BHXH không hợp lệ.");
            if (!string.IsNullOrEmpty(dto.MaBHYT) && dto.MaBHYT.Length < 5)
                errors.Add("Mã BHYT không hợp lệ.");

            if (dto.NgayCap > DateTime.Now)
                errors.Add("Ngày cấp bảo hiểm không được lớn hơn hiện tại.");

            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
