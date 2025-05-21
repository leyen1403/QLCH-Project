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
    public class DangKyCaService : IDangKyCaService
    {
        private readonly DangKyCaRepository _repo = new DangKyCaRepository();

        public List<DangKyCa> GetAll() => _repo.GetAll();

        public DangKyCa GetById(int id) => _repo.GetById(id);

        public bool Add(DangKyCa dk)
        {
            try
            {
                Validate(dk);
                _repo.Add(dk);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm đăng ký ca: {ex.Message}");
            }
        }

        public bool Update(DangKyCa dk)
        {
            try
            {
                Validate(dk);
                if (_repo.GetById(dk.MaDangKy) == null)
                    throw new Exception("Đăng ký ca không tồn tại.");
                _repo.Update(dk);
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
                    throw new Exception("Đăng ký ca không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa: {ex.Message}");
            }
        }

        private void Validate(DangKyCa dk)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(dk.MaNV)) errors.Add("Mã nhân viên không được để trống.");
            if (dk.MaCa <= 0) errors.Add("Mã ca làm việc không hợp lệ.");
            if (errors.Count > 0) throw new Exception(string.Join("\n", errors));
        }
    }
}
