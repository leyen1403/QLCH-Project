using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IChiTietPhieuNhapService
    {
        List<ChiTietPhieuNhap> GetAll();
        ChiTietPhieuNhap GetById(int id);
        bool Add(ChiTietPhieuNhap ct);
        bool Update(ChiTietPhieuNhap ct);
        bool Delete(int id);
    }
}
