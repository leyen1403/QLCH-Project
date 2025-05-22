using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Common.Enums
{
    public enum TrangThaiTaiKhoan
    {
        [Description("Hoạt động")]
        HoatDong,
        [Description("Đã khóa")]
        Khoa,
        [Description("Tạm ngừng")]
        TamNgung,
    }
}
