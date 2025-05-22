using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Common.Enums
{
    public enum TrangThaiHopDongLD
    {
        [Description("Còn hiệu lực")]
        ConHieuLuc,
        [Description("Hết hạn")]
        HetHan,
        [Description("Tạm dừng")]
        TamDung,
    }
}
