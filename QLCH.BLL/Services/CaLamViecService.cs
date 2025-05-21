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
    public class CaLamViecService : ICaLamViecService
    {
        private readonly CaLamViecRepository _repo = new CaLamViecRepository();

        public List<CaLamViec> GetAll() => _repo.GetAll();

        public CaLamViec GetById(int id) => _repo.GetById(id);

        public bool Add(CaLamViec ca)
        {
            try
            {
                Validate(ca);
                _repo.Add(ca);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm ca làm việc: {ex.Message}");
            }
        }

        public bool Update(CaLamViec ca)
        {
            try
            {
                Validate(ca);
                if (_repo.GetById(ca.MaCa) == null)
                    throw new Exception("Ca làm việc không tồn tại.");
                _repo.Update(ca);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật: {ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Ca làm việc không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa: {ex.Message}");
            }
        }

        private void Validate(CaLamViec ca)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(ca.TenCa))
                errors.Add("Tên ca không được để trống.");
            if (ca.GioBatDau >= ca.GioKetThuc)
                errors.Add("Giờ kết thúc phải lớn hơn giờ bắt đầu.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
