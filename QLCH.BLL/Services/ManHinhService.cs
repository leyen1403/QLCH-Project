using QLCH.BLL.Interfaces;
using QLCH.DAL.Models;
using QLCH.DAL.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Services
{
    public class ManHinhService : IManHinhService
    {
        private readonly ManHinhRepository _repo = new ManHinhRepository();

        public List<ManHinh> GetAll() => _repo.GetAll();

        public ManHinh GetById(int id) => _repo.GetById(id);

        public bool Add(ManHinh m)
        {
            try
            {
                Validate(m);
                _repo.Add(m);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm màn hình: {ex.Message}");
            }
        }

        public bool Update(ManHinh m)
        {
            try
            {
                Validate(m);
                if (_repo.GetById(m.MaManHinh) == null)
                    throw new Exception("Màn hình không tồn tại.");
                _repo.Update(m);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật màn hình: {ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Màn hình không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa màn hình: {ex.Message}");
            }
        }

        private void Validate(ManHinh m)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(m.TenManHinh))
                errors.Add("Tên màn hình không được để trống.");
            if (string.IsNullOrWhiteSpace(m.URL))
                errors.Add("URL không được để trống.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
