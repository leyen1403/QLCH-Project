// HopDongLaoDong.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class HopDongLaoDong
    {
        public int MaHopDong { get; set; }
        public string MaNV { get; set; }
        public string LoaiHopDong { get; set; }
        public DateTime NgayKy { get; set; }
        public DateTime NgayHieuLuc { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public decimal LuongCoBan { get; set; }
        public int ThoiHanHD { get; set; }
        public string TrangThai { get; set; }
    }
}
