using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IChiTietKhoService
    {
        List<ChiTietKho> GetAll();
        ChiTietKho GetById(int id);
        bool Add(ChiTietKho ct);
        bool Update(ChiTietKho ct);
        bool Delete(int id);
    }
}
