using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class ThuocTinhBienThe
    {
        public int MaThuocTinhBienThe { get; set; }
        public int MaBienThe { get; set; }
        public string LoaiThuocTinh { get; set; }
        public string GiaTri { get; set; }
    }
}
