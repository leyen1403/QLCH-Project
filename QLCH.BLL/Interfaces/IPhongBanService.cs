// IPhongBanService.cs
using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IPhongBanService
    {
        List<PhongBan> GetAll();
        PhongBan GetById(int id);
        bool Add(PhongBan pb);
        bool Update(PhongBan pb);
        bool Delete(int id);
    }
}
