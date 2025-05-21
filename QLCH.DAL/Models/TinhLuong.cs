using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class TinhLuong
    {
        public int MaBangLuong { get; set; }
        public string MaNV { get; set; }
        public string ThangNam { get; set; }
        public decimal LuongCoBan { get; set; }
        public decimal PhuCap { get; set; }
        public decimal TienOT { get; set; }
        public decimal TruPhat { get; set; }
        public decimal Thuong { get; set; }
        public decimal TongThuNhap { get; set; }
        public string TrangThai { get; set; }
        public DateTime? NgayThanhToan { get; set; }
    }
}
