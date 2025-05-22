using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class ChiTietBaoCaoTonKho
    {
        public int MaChiTiet { get; set; }
        public int MaBaoCaoTonKho { get; set; }
        public int MaBienThe { get; set; }
        public int SoLuongTon { get; set; }
        public string TrangThai { get; set; }
    }
}
