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
    public class ThuocTinhBienTheService : IThuocTinhBienTheService
    {
        private readonly ThuocTinhBienTheRepository _repo = new ThuocTinhBienTheRepository();

        public List<ThuocTinhBienThe> GetAll() => _repo.GetAll();

        public ThuocTinhBienThe GetById(int id) => _repo.GetById(id);

        public bool Add(ThuocTinhBienThe tt)
        {
            try
            {
                Validate(tt);
                _repo.Add(tt);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm thuộc tính: {ex.Message}");
            }
        }

        public bool Update(ThuocTinhBienThe tt)
        {
            try
            {
                Validate(tt);
                if (_repo.GetById(tt.MaThuocTinhBienThe) == null)
                    throw new Exception("Thuộc tính không tồn tại.");
                _repo.Update(tt);
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
                    throw new Exception("Thuộc tính không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa: {ex.Message}");
            }
        }

        private void Validate(ThuocTinhBienThe tt)
        {
            var errors = new List<string>();
            if (tt.MaBienThe <= 0)
                errors.Add("Mã biến thể không hợp lệ.");
            if (string.IsNullOrWhiteSpace(tt.LoaiThuocTinh) ||
               (tt.LoaiThuocTinh != "Màu sắc" && tt.LoaiThuocTinh != "Kích thước"))
                errors.Add("Loại thuộc tính phải là 'Màu sắc' hoặc 'Kích thước'.");
            if (string.IsNullOrWhiteSpace(tt.GiaTri))
                errors.Add("Giá trị không được để trống.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
