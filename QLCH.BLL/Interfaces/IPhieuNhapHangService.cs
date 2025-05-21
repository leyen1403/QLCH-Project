using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IPhieuNhapHangService
    {
        List<PhieuNhapHang> GetAll();
        PhieuNhapHang GetById(int id);
        bool Add(PhieuNhapHang ph);
        bool Update(PhieuNhapHang ph);
        bool Delete(int id);
    }
}
