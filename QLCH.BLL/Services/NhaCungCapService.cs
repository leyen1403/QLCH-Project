using QLCH.DAL;
using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL
{
    public class NhaCungCapService : INhaCungCapService
    {
        private readonly NhaCungCapRepository _nhaCungCapRepository;
        public NhaCungCapService()
        {
            _nhaCungCapRepository = new NhaCungCapRepository();
        }

        List<NhaCungCap> INhaCungCapService.GetAllNhaCungCap()
        {
            return _nhaCungCapRepository.GetAll();
        }
    }
}
