using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IPhieuThanhToanService
    {
        List<PhieuThanhToan> GetAll();
        PhieuThanhToan GetById(int id);
        bool Add(PhieuThanhToan pt);
        bool Update(PhieuThanhToan pt);
        bool Delete(int id);
        bool HuyPhieuThanhToan(int id);
    }
}
