using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class PhanQuyenManHinh
    {
        public int MaPhanQuyen { get; set; }
        public int MaTaiKhoan { get; set; }
        public int MaManHinh { get; set; }
        public bool CoQuyen { get; set; }
    }
}
