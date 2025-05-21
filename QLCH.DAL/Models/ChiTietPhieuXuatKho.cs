using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class ChiTietPhieuXuatKho
    {
        public int MaChiTietXK { get; set; }
        public int MaPhieuXuat { get; set; }
        public int MaBienThe { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaXuat { get; set; }
        public decimal ThanhTien { get; set; } // Computed
    }
}
