using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class BienTheSanPham
    {
        public int MaBienThe { get; set; }
        public int MaSanPham { get; set; }
        public string TenBienThe { get; set; }
        public string MoTa { get; set; }
        public decimal GiaNhap { get; set; }
        public decimal GiaBan { get; set; }
        public string TrangThai { get; set; }
    }
}
