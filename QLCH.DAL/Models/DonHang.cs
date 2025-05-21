using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class DonHang
    {
        public int MaDonHang { get; set; }
        public int? MaKhachHang { get; set; }
        public DateTime NgayDat { get; set; }
        public decimal TongTien { get; set; }
        public decimal GiamGia { get; set; }
        public decimal TongTienSauGiam { get; set; } // Computed
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
    }
}
