using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IThuocTinhBienTheService
    {
        List<ThuocTinhBienThe> GetAll();
        ThuocTinhBienThe GetById(int id);
        bool Add(ThuocTinhBienThe tt);
        bool Update(ThuocTinhBienThe tt);
        bool Delete(int id);
    }
}
