using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class PhieuKiemKe
    {
        public int MaPhieuKiemKe { get; set; }
        public int MaKho { get; set; }
        public DateTime NgayKiemKe { get; set; }
        public string NguoiKiemKe { get; set; }
        public string GhiChu { get; set; }
    }
}
