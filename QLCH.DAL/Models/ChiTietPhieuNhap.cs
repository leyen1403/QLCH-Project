using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class ChiTietPhieuNhap
    {
        public int MaChiTiet { get; set; }
        public int MaPhieuNhap { get; set; }
        public int MaBienThe { get; set; }
        public int SoLuongNhap { get; set; }
        public decimal DonGiaNhap { get; set; }
        public decimal ThanhTien { get; set; } // Computed column
    }
}
