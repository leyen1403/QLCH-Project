using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.DTO
{
    public class NhanVienDTO
    {
        // Thông tin nhân viên
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string CMND { get; set; }
        public string MaSoThue { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public int MaChucVu { get; set; }
        public int MaPhongBan { get; set; }
        public int MaCuaHang { get; set; }
        public string LoaiHopDong { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayVaoLam { get; set; }
        public DateTime NgayNghiViec { get; set; }

        // Hợp đồng lao động
        public int? MaHopDong { get; set; }
        public decimal? LuongCoBan { get; set; }
        public int? ThoiHanHD { get; set; }
        public DateTime NgayKy { get; set; }
        public DateTime NgayHieuLuc { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string TrangThaiHopDong { get; set; }

        // Bảo hiểm
        public int? MaBH { get; set; }
        public string MaBHXH { get; set; }
        public string MaBHYT { get; set; }
        public string NhaCungCap { get; set; }
        public DateTime NgayCap { get; set; }
        public string TrangThaiBaoHiem { get; set; }
    }
}
