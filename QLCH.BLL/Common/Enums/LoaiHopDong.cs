using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Common.Enums
{
    public enum LoaiHopDong
    {
        [Description("Thử việc")]
        ThuViec,
        [Description("Chính thức")]
        ChinhThuc,
        [Description("Thời vụ")]
        ThoiVu,
        [Description("Bán thời gian")]
        BanThoiGian,
    }
}
