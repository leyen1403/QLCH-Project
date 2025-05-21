using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class ChiTietKhuyenMai
    {
        public int MaChiTietKM { get; set; }
        public int MaDonHang { get; set; }
        public int MaKhuyenMai { get; set; }
        public decimal GiaTriGiam { get; set; }
    }
}
