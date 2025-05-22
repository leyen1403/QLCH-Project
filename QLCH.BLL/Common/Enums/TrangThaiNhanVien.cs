using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Common.Enums
{
    public enum TrangThaiNhanVien
    {
        [Description("Đang làm việc")]
        DangLamViec,
        [Description("Nghỉ việc")]
        NghiViec,
        [Description("Thử việc")]
        ThuViec,
    }
}
