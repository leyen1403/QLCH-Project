using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IChiTietPhieuKiemKeService
    {
        List<ChiTietPhieuKiemKe> GetAll();
        ChiTietPhieuKiemKe GetById(int id);
        bool Add(ChiTietPhieuKiemKe ct);
        bool Update(ChiTietPhieuKiemKe ct);
        bool Delete(int id);
    }
}
