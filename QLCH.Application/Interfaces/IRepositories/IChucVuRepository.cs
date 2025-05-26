using QLCH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.Application.Interfaces.IRepositories
{
    public interface IChucVuRepository
    {
        List<ChucVu> GetAll();
        ChucVu? GetById(int id);
        int Add(ChucVu cv);
        void Update(ChucVu cv);
        void Delete(int id);
    }
}
