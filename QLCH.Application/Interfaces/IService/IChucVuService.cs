using QLCH.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.Application.Interfaces.IService
{
    public interface IChucVuService
    {
        List<ChucVuDto> GetAll();
        ChucVuDto? GetById(int id);
        int Create(ChucVuDto dto);
        void Update(ChucVuDto dto);
        void Delete(int id);
    }
}
