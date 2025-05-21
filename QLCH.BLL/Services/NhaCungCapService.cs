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
    public class NhaCungCapService : INhaCungCapService
    {
        private readonly NhaCungCapRepository _repo = new NhaCungCapRepository();

        public List<NhaCungCap> GetAll() => _repo.GetAll();

        public NhaCungCap GetById(int id) => _repo.GetById(id);

        public bool Add(NhaCungCap ncc)
        {
            try
            {
                Validate(ncc);
                _repo.Add(ncc);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm nhà cung cấp: {ex.Message}");
            }
        }

        public bool Update(NhaCungCap ncc)
        {
            try
            {
                Validate(ncc);
                if (_repo.GetById(ncc.MaNCC) == null)
                    throw new Exception("Nhà cung cấp không tồn tại.");
                _repo.Update(ncc);
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
                    throw new Exception("Nhà cung cấp không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa: {ex.Message}");
            }
        }

        private void Validate(NhaCungCap ncc)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(ncc.TenNCC))
                errors.Add("Tên nhà cung cấp không được để trống.");
            if (string.IsNullOrWhiteSpace(ncc.SoDienThoai))
                errors.Add("Số điện thoại không được để trống.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
