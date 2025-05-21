using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class ChiTietDonHang
    {
        public int MaChiTietDH { get; set; }
        public int MaDonHang { get; set; }
        public int MaBienThe { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaBan { get; set; }
        public decimal ThanhTien { get; set; } // Computed
    }
}
