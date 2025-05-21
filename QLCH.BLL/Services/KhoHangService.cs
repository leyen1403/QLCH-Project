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
    public class KhoHangService : IKhoHangService
    {
        private readonly KhoHangRepository _repo = new KhoHangRepository();

        public List<KhoHang> GetAll() => _repo.GetAll();

        public KhoHang GetById(int id) => _repo.GetById(id);

        public bool Add(KhoHang kho)
        {
            try
            {
                Validate(kho);
                _repo.Add(kho);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm kho: {ex.Message}");
            }
        }

        public bool Update(KhoHang kho)
        {
            try
            {
                Validate(kho);
                if (_repo.GetById(kho.MaKho) == null)
                    throw new Exception("Kho không tồn tại.");
                _repo.Update(kho);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật kho: {ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Kho không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa kho: {ex.Message}");
            }
        }

        private void Validate(KhoHang kho)
        {
            var errors = new List<string>();
            if (kho.MaCuaHang <= 0)
                errors.Add("Mã cửa hàng không hợp lệ.");
            if (string.IsNullOrWhiteSpace(kho.TenKho))
                errors.Add("Tên kho không được để trống.");
            if (string.IsNullOrWhiteSpace(kho.DiaChi))
                errors.Add("Địa chỉ không được để trống.");
            if (string.IsNullOrWhiteSpace(kho.SoDienThoai))
                errors.Add("Số điện thoại không được để trống.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
