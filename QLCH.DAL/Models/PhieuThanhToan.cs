using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class PhieuThanhToan
    {
        public int MaPhieuThanhToan { get; set; }
        public int MaDonHang { get; set; }
        public DateTime NgayThanhToan { get; set; }
        public decimal SoTienThanhToan { get; set; }
        public string PhuongThuc { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
    }
}
