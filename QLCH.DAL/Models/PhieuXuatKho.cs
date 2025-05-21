using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class PhieuXuatKho
    {
        public int MaPhieuXuat { get; set; }
        public int MaKhoXuat { get; set; }
        public int? MaKhoNhan { get; set; } // nullable
        public int? MaNCC { get; set; }     // nullable
        public DateTime NgayXuat { get; set; }
        public string LoaiXuat { get; set; }
        public decimal TongTien { get; set; }
        public string GhiChu { get; set; }
    }
}
