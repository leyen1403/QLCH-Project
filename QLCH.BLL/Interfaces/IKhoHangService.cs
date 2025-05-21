using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IKhoHangService
    {
        List<KhoHang> GetAll();
        KhoHang GetById(int id);
        bool Add(KhoHang kho);
        bool Update(KhoHang kho);
        bool Delete(int id);
    }
}
