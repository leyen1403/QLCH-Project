using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class KhachHang
    {
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
        public bool TrangThai { get; set; }
        public int DiemTichLuy { get; set; }
        public decimal TongChiTieu { get; set; }
        public bool LoaiKhachHang { get; set; }
        public decimal PhanTramGiamGia { get; set; }
        public decimal CongNo { get; set; }
        public int SoLanGheTham { get; set; }
        public int SoLanMuaHang { get; set; }
        public string GhiChu { get; set; }

        public KhachHang()
        {
            MaKH = string.Empty;
            TenKH = string.Empty;
            DiaChi = string.Empty;
            SDT = string.Empty;
            Email = string.Empty;
            ThoiGianTao = DateTime.Now;
            ThoiGianCapNhat = DateTime.Now;
            TrangThai = true;
            DiemTichLuy = 0;
            TongChiTieu = 0;
            LoaiKhachHang = false; // Mặc định là khách hàng thường
            PhanTramGiamGia = 0;
            CongNo = 0;
            SoLanGheTham = 0;
            SoLanMuaHang = 0;
            GhiChu = string.Empty;
        }
    }
}
