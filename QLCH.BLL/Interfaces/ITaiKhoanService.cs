using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface ITaiKhoanService
    {
        List<TaiKhoan> GetAll();
        TaiKhoan GetById(int id);
        bool Add(TaiKhoan tk);
        bool Update(TaiKhoan tk);
        bool Delete(int id);
        TaiKhoan Login(string username, string password);
        bool ChangePassword(int id, string newPassword);
    }
}
