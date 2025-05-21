using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class ChiTietPhieuKiemKe
    {
        public int MaChiTietKK { get; set; }
        public int MaPhieuKiemKe { get; set; }
        public int MaBienThe { get; set; }
        public int SoLuongThucTe { get; set; }
        public int? SoLuongChenhLech { get; set; }
    }
}
