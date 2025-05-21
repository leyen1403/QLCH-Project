using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IDonHangService
    {
        List<DonHang> GetAll();
        DonHang GetById(int id);
        bool Add(DonHang dh);
        bool Update(DonHang dh);
        bool Delete(int id);
    }
}
