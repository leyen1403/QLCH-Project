using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IPhieuKiemKeService
    {
        List<PhieuKiemKe> GetAll();
        PhieuKiemKe GetById(int id);
        bool Add(PhieuKiemKe pk);
        bool Update(PhieuKiemKe pk);
        bool Delete(int id);
    }
}
