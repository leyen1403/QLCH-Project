using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IChamCongService
    {
        List<ChamCong> GetAll();
        ChamCong GetById(int id);
        bool Add(ChamCong cc);
        bool Update(ChamCong cc);
        bool Delete(int id);
    }
}
