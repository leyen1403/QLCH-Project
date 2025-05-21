using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class PhieuNhapHang
    {
        public int MaPhieuNhap { get; set; }
        public int MaKho { get; set; }
        public DateTime NgayNhap { get; set; }
        public string NguoiNhap { get; set; }
        public decimal TongTien { get; set; }
        public string GhiChu { get; set; }
    }
}
