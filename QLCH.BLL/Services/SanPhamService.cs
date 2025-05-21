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
    public class SanPhamService : ISanPhamService
    {
        private readonly SanPhamRepository _repo = new SanPhamRepository();

        public List<SanPham> GetAll() => _repo.GetAll();

        public SanPham GetById(int id) => _repo.GetById(id);

        public bool Add(SanPham sp)
        {
            try
            {
                Validate(sp);
                _repo.Add(sp);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm sản phẩm: {ex.Message}");
            }
        }

        public bool Update(SanPham sp)
        {
            try
            {
                Validate(sp);
                if (_repo.GetById(sp.MaSanPham) == null)
                    throw new Exception("Sản phẩm không tồn tại.");
                _repo.Update(sp);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật sản phẩm: {ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Sản phẩm không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa sản phẩm: {ex.Message}");
            }
        }

        private void Validate(SanPham sp)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(sp.TenSanPham))
                errors.Add("Tên sản phẩm không được để trống.");
            if (sp.MaNCC <= 0)
                errors.Add("Mã nhà cung cấp không hợp lệ.");
            if (!string.IsNullOrWhiteSpace(sp.TrangThai) &&
                sp.TrangThai != "Hoạt động" && sp.TrangThai != "Ngừng kinh doanh")
                errors.Add("Trạng thái không hợp lệ.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
