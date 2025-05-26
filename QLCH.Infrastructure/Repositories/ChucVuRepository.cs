using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QLCH.Application.Interfaces.IRepositories;
using QLCH.Domain.Entities;
using QLCH.Infrastructure.Persistence;

namespace QLCH.Infrastructure.Repositories
{
    public class ChucVuRepository : IChucVuRepository
    {
        private readonly QLCHDbContext _context;

        public ChucVuRepository(QLCHDbContext context)
        {
            _context = context;
        }

        public List<ChucVu> GetAll()
        {
            return _context.ChucVus.AsNoTracking().ToList();
        }

        public ChucVu? GetById(int id)
        {
            return _context.ChucVus.AsNoTracking().FirstOrDefault(c => c.MaChucVu == id);
        }

        public void Add(ChucVu cv)
        {
            _context.ChucVus.Add(cv);
            _context.SaveChanges();
        }

        public void Update(ChucVu cv)
        {
            _context.ChucVus.Update(cv);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.ChucVus.Find(id);
            if (entity != null)
            {
                _context.ChucVus.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
