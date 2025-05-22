// IHopDongLaoDongService.cs
using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IHopDongLaoDongService
    {
        List<HopDongLaoDong> GetAll();
        HopDongLaoDong GetById(int id);
        bool Add(HopDongLaoDong hd);
        bool Update(HopDongLaoDong hd);
        bool Delete(int id);
        HopDongLaoDong GetByMaNV(string maNV);
    }
}
