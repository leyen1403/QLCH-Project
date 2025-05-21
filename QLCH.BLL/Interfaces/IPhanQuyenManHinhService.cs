using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IPhanQuyenManHinhService
    {
        List<PhanQuyenManHinh> GetAll();
        PhanQuyenManHinh GetById(int id);
        bool Add(PhanQuyenManHinh pq);
        bool Update(PhanQuyenManHinh pq);
        bool Delete(int id);
    }
}
