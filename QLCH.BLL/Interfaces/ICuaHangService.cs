using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface ICuaHangService
    {
        List<CuaHang> GetAll();
        CuaHang GetById(int id);
        bool Add(CuaHang ch);
        bool Update(CuaHang ch);
        bool Delete(int id);
    }
}
