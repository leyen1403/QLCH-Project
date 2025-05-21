// CuaHangService.cs
using QLCH.DAL.Models;
using QLCH.DAL.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Services
{
    public class CuaHangService
    {
        private readonly CuaHangRepository _repo = new CuaHangRepository();

        public List<CuaHang> GetAll()
        {
            return _repo.GetAll();
        }

        public CuaHang GetById(int id)
        {
            return _repo.GetById(id);
        }

        public bool Add(CuaHang ch)
        {
            try
            {
                Validate(ch);
                _repo.Add(ch);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm cửa hàng: {ex.Message}");
                return false;
            }
        }

        public bool Update(CuaHang ch)
        {
            try
            {
                Validate(ch);
                if (_repo.GetById(ch.MaCuaHang) == null)
                    throw new Exception("Cửa hàng không tồn tại.");
                _repo.Update(ch);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Cửa hàng không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xóa: {ex.Message}");
                return false;
            }
        }

        private void Validate(CuaHang ch)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(ch.TenCuaHang))
                errors.Add("Tên cửa hàng không được để trống.");
            if (string.IsNullOrWhiteSpace(ch.DiaChi))
                errors.Add("Địa chỉ không được để trống.");
            if (string.IsNullOrWhiteSpace(ch.SoDienThoai) || ch.SoDienThoai.Length < 10)
                errors.Add("Số điện thoại không hợp lệ.");

            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
