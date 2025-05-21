using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface ILichSuDieuChuyenService
    {
        List<LichSuDieuChuyen> GetAll();
        LichSuDieuChuyen GetById(int id);
        bool Add(LichSuDieuChuyen ls);
        bool Update(LichSuDieuChuyen ls);
        bool Delete(int id);
    }
}
