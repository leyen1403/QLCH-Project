// INhanVienService.cs
using QLCH.BLL.DTO;
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
        NhanVienDTO GetNhanVienFull(string maNV);
        bool AddNhanVienFull(NhanVien nv, HopDongLaoDong hd, BaoHiem bh);
        bool UpdateNhanVienFull(NhanVien nv, HopDongLaoDong hd, BaoHiem bh);
    }
}
