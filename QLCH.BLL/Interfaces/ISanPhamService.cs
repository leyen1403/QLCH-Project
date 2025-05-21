using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface ISanPhamService
    {
        List<SanPham> GetAll();
        SanPham GetById(int id);
        bool Add(SanPham sp);
        bool Update(SanPham sp);
        bool Delete(int id);
    }
}
