using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class ChiTietKho
    {
        public int MaChiTietKho { get; set; }
        public int MaKho { get; set; }
        public int MaBienThe { get; set; }
        public int SoLuong { get; set; }
        public string TrangThai { get; set; }
    }
}
