using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class BaoCaoTonKho
    {
        public int MaBaoCaoTonKho { get; set; }
        public int MaKho { get; set; }
        public DateTime ThoiGian { get; set; }
        public int TongSanPham { get; set; }
        public int TongSoLuong { get; set; }
        public string GhiChu { get; set; }
    }
}
