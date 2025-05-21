using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class LichSuDieuChuyen
    {
        public int MaDieuChuyen { get; set; }
        public string MaNV { get; set; }
        public int MaPhongBanMoi { get; set; }
        public int MaCuaHangMoi { get; set; }
        public DateTime NgayDieuChuyen { get; set; }
        public string LyDo { get; set; }
    }
}
