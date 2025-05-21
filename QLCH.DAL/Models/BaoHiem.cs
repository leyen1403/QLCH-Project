using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class BaoHiem
    {
        public int MaBaoHiem { get; set; }
        public string MaNV { get; set; }
        public string SoBHXH { get; set; }
        public string SoBHYT { get; set; }
        public DateTime NgayCap { get; set; }
        public string NhaCungCap { get; set; }
        public string TrangThai { get; set; }
    }
}
