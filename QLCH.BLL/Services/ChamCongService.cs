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
    public class ChamCongService : IChamCongService
    {
        private readonly ChamCongRepository _repo = new ChamCongRepository();

        public List<ChamCong> GetAll()
        {
            return _repo.GetAll();
        }

        public ChamCong GetById(int id)
        {
            return _repo.GetById(id);
        }

        public bool Add(ChamCong cc)
        {
            try
            {
                Validate(cc);
                _repo.Add(cc);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm chấm công: {ex.Message}");
                throw new Exception($"Lỗi khi thêm chấm công: {ex.Message}");
            }
        }

        public bool Update(ChamCong cc)
        {
            try
            {
                Validate(cc);
                if (_repo.GetById(cc.MaChamCong) == null)
                    throw new Exception("Chấm công không tồn tại.");
                _repo.Update(cc);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật chấm công: {ex.Message}");
                throw new Exception($"Lỗi khi cập nhật chấm công: {ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Chấm công không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xóa: {ex.Message}");
                throw new Exception($"Lỗi khi xóa chấm công: {ex.Message}");
            }
        }

        private void Validate(ChamCong cc)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(cc.MaNV))
                errors.Add("Mã nhân viên không được để trống.");
            if (cc.Ngay == default)
                errors.Add("Ngày chấm công không hợp lệ.");
            if (string.IsNullOrWhiteSpace(cc.LoaiCa))
                errors.Add("Loại ca không được để trống.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
