using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IBienTheSanPhamService
    {
        List<BienTheSanPham> GetAll();
        BienTheSanPham GetById(int id);
        bool Add(BienTheSanPham bts);
        bool Update(BienTheSanPham bts);
        bool Delete(int id);
    }
}
