using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IKhuyenMaiService
    {
        List<KhuyenMai> GetAll();
        KhuyenMai GetById(int id);
        bool Add(KhuyenMai km);
        bool Update(KhuyenMai km);
        bool Delete(int id);
    }
}
