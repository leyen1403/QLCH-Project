using QLCH.BLL.Interfaces;
using QLCH.DAL;
using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Services
{
    public class ManHinhService : IManHinhService
    {
        private readonly ManHinhRepository _manHinhRepository;
        public ManHinhService()
        {
            _manHinhRepository = new ManHinhRepository();
        }
        public bool Add(ManHinh manHinh)
        {
            return _manHinhRepository.Add(manHinh);
        }

        public List<ManHinh> GetAll()
        {
            return _manHinhRepository.GetAll();
        }

        public ManHinh GetById(string maMH)
        {
            return _manHinhRepository.GetById(maMH);
        }
    }
}
