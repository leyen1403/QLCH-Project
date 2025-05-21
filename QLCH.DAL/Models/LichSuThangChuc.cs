using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class LichSuThangChuc
    {
        public int MaLichSu { get; set; }
        public string MaNV { get; set; }
        public int MaChucVuMoi { get; set; }
        public DateTime NgayThangChuc { get; set; }
        public string LyDo { get; set; }
    }
}
