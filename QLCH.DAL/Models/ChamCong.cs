using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class ChamCong
    {
        public int MaChamCong { get; set; }
        public string MaNV { get; set; }
        public DateTime Ngay { get; set; }
        public TimeSpan? GioVao { get; set; }
        public TimeSpan? GioRa { get; set; }
        public decimal? SoGioLam { get; set; }
        public string LoaiCa { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
    }
}
