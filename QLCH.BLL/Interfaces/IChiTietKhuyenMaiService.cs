using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IChiTietKhuyenMaiService
    {
        List<ChiTietKhuyenMai> GetAll();
        ChiTietKhuyenMai GetById(int id);
        bool Add(ChiTietKhuyenMai ct);
        bool Update(ChiTietKhuyenMai ct);
        bool Delete(int id);
    }
}
