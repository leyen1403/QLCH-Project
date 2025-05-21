using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IDangKyCaService
    {
        List<DangKyCa> GetAll();
        DangKyCa GetById(int id);
        bool Add(DangKyCa dk);
        bool Update(DangKyCa dk);
        bool Delete(int id);
    }
}
