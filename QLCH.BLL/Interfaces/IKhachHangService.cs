using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLCH.DAL.Models;
using QLCH.DAL.Repositorys;

namespace QLCH.BLL.Interfaces
{
    public interface IKhachHangService
    {
        List<KhachHang> GetAll();
        KhachHang GetByID(string MaKH);
        void Add(KhachHang khachHang);
        void Update(KhachHang khachHang);
        void Delete(string MaKH);
        List<KhachHang> Search(string keyword);
        List<KhachHang> Sort(string sortBy);
    }
}
