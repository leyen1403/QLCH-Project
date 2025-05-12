using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public enum TrangThaiNhaCungCap
    {
        KhongHoatDong = 0,
        HoatDong = 1
    }

    public class NhaCungCap
    {
        public string MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
        public TrangThaiNhaCungCap TrangThai { get; set; } // 0: không hoạt động, 1: hoạt động  
    }
}
