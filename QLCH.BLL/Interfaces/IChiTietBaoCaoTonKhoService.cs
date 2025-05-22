using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IChiTietBaoCaoTonKhoService
    {
        List<ChiTietBaoCaoTonKho> GetAll();
        ChiTietBaoCaoTonKho GetById(int id);
        bool Add(ChiTietBaoCaoTonKho ct);
        bool Update(ChiTietBaoCaoTonKho ct);
        bool Delete(int id);
    }
}
