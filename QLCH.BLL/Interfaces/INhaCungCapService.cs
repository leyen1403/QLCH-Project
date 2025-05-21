using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface INhaCungCapService
    {
        List<NhaCungCap> GetAll();
        NhaCungCap GetById(int id);
        bool Add(NhaCungCap ncc);
        bool Update(NhaCungCap ncc);
        bool Delete(int id);
    }
}
