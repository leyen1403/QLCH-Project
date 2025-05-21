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
    public class BienTheSanPhamService : IBienTheSanPhamService
    {
        private readonly BienTheSanPhamRepository _repo = new BienTheSanPhamRepository();

        public List<BienTheSanPham> GetAll() => _repo.GetAll();

        public BienTheSanPham GetById(int id) => _repo.GetById(id);

        public bool Add(BienTheSanPham bts)
        {
            try
            {
                Validate(bts);
                _repo.Add(bts);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm biến thể: {ex.Message}");
            }
        }

        public bool Update(BienTheSanPham bts)
        {
            try
            {
                Validate(bts);
                if (_repo.GetById(bts.MaBienThe) == null)
                    throw new Exception("Biến thể không tồn tại.");
                _repo.Update(bts);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật biến thể: {ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Biến thể không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa biến thể: {ex.Message}");
            }
        }

        private void Validate(BienTheSanPham bts)
        {
            var errors = new List<string>();
            if (bts.MaSanPham <= 0) errors.Add("Mã sản phẩm không hợp lệ.");
            if (string.IsNullOrWhiteSpace(bts.TenBienThe)) errors.Add("Tên biến thể không được để trống.");
            if (bts.GiaNhap < 0) errors.Add("Giá nhập phải >= 0.");
            if (bts.GiaBan < 0) errors.Add("Giá bán phải >= 0.");
            if (!string.IsNullOrWhiteSpace(bts.TrangThai) &&
                bts.TrangThai != "Hoạt động" && bts.TrangThai != "Hết hàng")
                errors.Add("Trạng thái không hợp lệ.");
            if (errors.Count > 0) throw new Exception(string.Join("\n", errors));
        }
    }
}
