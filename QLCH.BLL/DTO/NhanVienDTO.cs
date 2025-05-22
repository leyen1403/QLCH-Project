using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.DTO
{
    public class NhanVienDTO
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public string CMND { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string TrangThai { get; set; }

        public int MaPhongBan { get; set; }
        public int MaChucVu { get; set; }
        public int MaCuaHang { get; set; }

        public string LoaiHopDong { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }

        public string MaBHXH { get; set; }
        public string MaBHYT { get; set; }
        public DateTime NgayCap { get; set; }
    }
}
