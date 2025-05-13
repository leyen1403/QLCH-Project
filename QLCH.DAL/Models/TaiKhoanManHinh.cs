using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class TaiKhoanManHinh
    {
        public string MaTK { get; set; }
        public string MaMH { get; set; }
        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
        public bool TrangThai { get; set; }
    }
}
