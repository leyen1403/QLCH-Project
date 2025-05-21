// IChucVuService.cs
using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IChucVuService
    {
        List<ChucVu> GetAll();
        ChucVu GetById(int id);
        bool Add(ChucVu chucVu);
        bool Update(ChucVu chucVu);
        bool Delete(int id);
    }
}
