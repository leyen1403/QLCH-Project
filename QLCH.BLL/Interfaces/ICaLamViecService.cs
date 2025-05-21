using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface ICaLamViecService
    {
        List<CaLamViec> GetAll();
        CaLamViec GetById(int id);
        bool Add(CaLamViec ca);
        bool Update(CaLamViec ca);
        bool Delete(int id);
    }
}
