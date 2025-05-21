// NhanVien.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class NhanVien
    {
        public string MaNV { get; set; }

        public string HoTen { get; set; }

        public DateTime NgaySinh { get; set; }

        public string GioiTinh { get; set; }

        public string CMND_CCCD { get; set; }

        public string MaSoThue { get; set; }

        public string SoDienThoai { get; set; }

        public string Email { get; set; }

        public string DiaChi { get; set; }

        public int MaChucVu { get; set; }

        public int MaPhongBan { get; set; }

        public int MaCuaHang { get; set; }

        public string LoaiHopDong { get; set; }

        public string TrangThai { get; set; } = "Đang làm việc";

        public DateTime NgayVaoLam { get; set; }

        public DateTime? NgayNghiViec { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
