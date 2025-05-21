using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class DangKyCa
    {
        public int MaDangKy { get; set; }
        public string MaNV { get; set; }
        public int MaCa { get; set; }
        public DateTime Ngay { get; set; }
        public string TrangThai { get; set; }
        public string LyDo { get; set; }
    }
}
