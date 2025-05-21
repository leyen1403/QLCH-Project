using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IChiTietDonHangService
    {
        List<ChiTietDonHang> GetAll();
        ChiTietDonHang GetById(int id);
        bool Add(ChiTietDonHang ct);
        bool Update(ChiTietDonHang ct);
        bool Delete(int id);
    }
}
