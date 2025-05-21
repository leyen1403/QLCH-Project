using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IManHinhService
    {
        List<ManHinh> GetAll();
        ManHinh GetById(int id);
        bool Add(ManHinh m);
        bool Update(ManHinh m);
        bool Delete(int id);
    }
}
