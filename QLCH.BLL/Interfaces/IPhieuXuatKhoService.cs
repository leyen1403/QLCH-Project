using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IPhieuXuatKhoService
    {
        List<PhieuXuatKho> GetAll();
        PhieuXuatKho GetById(int id);
        bool Add(PhieuXuatKho px);
        bool Update(PhieuXuatKho px);
        bool Delete(int id);
    }
}
