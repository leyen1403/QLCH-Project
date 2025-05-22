using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Models
{
    public class BaoHiem
    {
        [Required(ErrorMessage = "Mã bảo hiểm là bắt buộc")]
        public int MaBaoHiem { get; set; }

        [Required(ErrorMessage = "Mã nhân viên là bắt buộc")]
        public string MaNV { get; set; }

        [Required(ErrorMessage = "Số bảo hiểm xã hội là bắt buộc")]
        public string SoBHXH { get; set; }

        [Required(ErrorMessage = "Số bảo hiểm y tế là bắt buộc")]
        public string SoBHYT { get; set; }

        public DateTime NgayCap { get; set; }

        [Required(ErrorMessage = "Nhà cung cấp là bắt buộc")]
        public string NhaCungCap { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc")]
        [RegularExpression("^(Còn hiệu lực|Hết hạn|Tạm dừng)$", ErrorMessage = "Trạng thái không hợp lệ")]
        public string TrangThai { get; set; }
    }
}
