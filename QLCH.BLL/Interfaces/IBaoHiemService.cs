using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IBaoHiemService
    {
        List<BaoHiem> GetAll();
        BaoHiem GetById(int id);
        bool Add(BaoHiem bh);
        bool Update(BaoHiem bh);
        bool Delete(int id);
    }
}
