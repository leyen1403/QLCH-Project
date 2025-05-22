using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Interfaces
{
    public interface IBaoCaoTonKhoService
    {
        List<BaoCaoTonKho> GetAll();
        BaoCaoTonKho GetById(int id);
        bool Add(BaoCaoTonKho bc);
        bool Update(BaoCaoTonKho bc);
        bool Delete(int id);
    }
}
