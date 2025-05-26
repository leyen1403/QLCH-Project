using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.Domain.Entities
{
    public class ChucVu
    {
        public int MaChucVu { get; set; }
        public string TenChucVu { get; set; } = string.Empty;
        public decimal HeSoLuong { get; set; }
        public string? MoTa { get; set; }
    }
}
