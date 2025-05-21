using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface ILichSuThangChucService
    {
        List<LichSuThangChuc> GetAll();
        LichSuThangChuc GetById(int id);
        bool Add(LichSuThangChuc ls);
        bool Update(LichSuThangChuc ls);
        bool Delete(int id);
    }
}
