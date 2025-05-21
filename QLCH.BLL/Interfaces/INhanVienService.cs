// INhanVienService.cs
using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface INhanVienService
    {
        List<NhanVien> GetAllNhanViens();
        NhanVien GetNhanVienById(string id);
        bool AddNhanVien(NhanVien nhanVien);
        bool UpdateNhanVien(NhanVien nhanVien);
        bool DeleteNhanVien(string id);
    }
}
