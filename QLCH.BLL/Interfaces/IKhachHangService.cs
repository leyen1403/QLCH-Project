using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IKhachHangService
    {
        List<KhachHang> GetAll();
        KhachHang GetById(int id);
        bool Add(KhachHang kh);
        bool Update(KhachHang kh);
        bool Delete(int id);
    }
}
