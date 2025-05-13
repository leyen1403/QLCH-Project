using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface ITaiKhoanManHinh
    {
        void Add(TaiKhoanManHinh taiKhoanManHinh);
        void Update(TaiKhoanManHinh taiKhoanManHinh);
        void Delete(string maTK, string maMH);
        TaiKhoanManHinh GetByID(string maTK, string maMH);
        List<TaiKhoanManHinh> GetAll();
    }
}
