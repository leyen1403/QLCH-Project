using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IChiTietPhieuXuatKhoService
    {
        List<ChiTietPhieuXuatKho> GetAll();
        ChiTietPhieuXuatKho GetById(int id);
        bool Add(ChiTietPhieuXuatKho ct);
        bool Update(ChiTietPhieuXuatKho ct);
        bool Delete(int id);
    }
}
