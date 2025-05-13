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
        // Get All KhachHang
        List<KhachHang> GetAll();
        // Get By Id
        KhachHang GetByID(string MaKH);
        // Add KhachHang
        void Add(KhachHang khachHang);
        // Update KhachHang
        void Update(KhachHang khachHang);
        // Delete KhachHang
        void Delete(string MaKH);
        // Search KhachHang
        List<KhachHang> Search(string keyword);
        // Sort KhachHang
        List<KhachHang> Sort(string sortBy);
    }
}
