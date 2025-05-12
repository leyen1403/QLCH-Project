using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    enum TrangThaiManHinh
    {
        KhongHoatDong = 0,
        HoatDong = 1
    }
    public class ManHinh
    {
        public string MaMH { get; set; }
        public string TenMH { get; set; }
        public string MoTa { get; set; }
        public string HinhAnh { get; set; }
        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
        public bool TrangThai { get; set; } // 0: không hoạt động, 1: hoạt động
    }
}
