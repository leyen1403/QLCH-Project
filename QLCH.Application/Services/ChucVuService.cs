using QLCH.Application.DTOs;
using QLCH.Application.Interfaces.IRepositories;
using QLCH.Application.Interfaces.IService;
using QLCH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.Application.Services
{
    public class ChucVuService : IChucVuService
    {
        private readonly IChucVuRepository _repo;

        public ChucVuService(IChucVuRepository repo)
        {
            _repo = repo;
        }

        public List<ChucVuDto> GetAll() =>
            _repo.GetAll().Select(cv => new ChucVuDto
            {
                MaChucVu = cv.MaChucVu,
                TenChucVu = cv.TenChucVu,
                HeSoLuong = cv.HeSoLuong,
                MoTa = cv.MoTa
            }).ToList();

        public ChucVuDto? GetById(int id)
        {
            var cv = _repo.GetById(id);
            if (cv == null) return null;
            return new ChucVuDto
            {
                MaChucVu = cv.MaChucVu,
                TenChucVu = cv.TenChucVu,
                HeSoLuong = cv.HeSoLuong,
                MoTa = cv.MoTa
            };
        }

        public int Create(ChucVuDto dto)
        {
            var entity = new ChucVu
            {
                TenChucVu = dto.TenChucVu,
                HeSoLuong = dto.HeSoLuong,
                MoTa = dto.MoTa
            };
            return _repo.Add(entity);
        }

        public void Update(ChucVuDto dto)
        {
            var entity = new ChucVu
            {
                MaChucVu = dto.MaChucVu ?? 0,
                TenChucVu = dto.TenChucVu,
                HeSoLuong = dto.HeSoLuong,
                MoTa = dto.MoTa
            };
            _repo.Update(entity);
        }

        public void Delete(int id) => _repo.Delete(id);
    }
}
