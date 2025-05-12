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
        ManHinh GetById(string maMH);
        bool Add(ManHinh manHinh);
    }
}
