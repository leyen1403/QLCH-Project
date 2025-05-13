using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface ITaiKhoan
    {
        void Add(TaiKhoan taiKhoan);
        void Update(TaiKhoan taiKhoan);
        void Delete(string maTK);
        TaiKhoan GetByID(string maTK);
        List<TaiKhoan> GetAll();
    }
}
