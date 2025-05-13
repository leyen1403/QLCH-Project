using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLCH.DAL.Models;

namespace QLCH.BLL.Interfaces
{
    public interface INhanVienService
    {
        void Add(NhanVien nhanVien);
        NhanVien GetByID(string maNV);
        List<NhanVien> GetAll();
        void Update(NhanVien nhanVien);
        void Delete(string maNV);
    }
}
