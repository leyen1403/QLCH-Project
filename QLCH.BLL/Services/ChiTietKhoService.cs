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
    public class ChiTietKhoService : IChiTietKhoService
    {
        private readonly ChiTietKhoRepository _repo = new ChiTietKhoRepository();

        public List<ChiTietKho> GetAll() => _repo.GetAll();

        public ChiTietKho GetById(int id) => _repo.GetById(id);

        public bool Add(ChiTietKho ct)
        {
            try
            {
                Validate(ct);
                _repo.Add(ct);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm chi tiết kho: {ex.Message}");
            }
        }

        public bool Update(ChiTietKho ct)
        {
            try
            {
                Validate(ct);
                if (_repo.GetById(ct.MaChiTietKho) == null)
                    throw new Exception("Chi tiết kho không tồn tại.");
                _repo.Update(ct);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật chi tiết kho: {ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Chi tiết kho không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa chi tiết kho: {ex.Message}");
            }
        }

        private void Validate(ChiTietKho ct)
        {
            var errors = new List<string>();
            if (ct.MaKho <= 0) errors.Add("Mã kho không hợp lệ.");
            if (ct.MaBienThe <= 0) errors.Add("Mã biến thể không hợp lệ.");
            if (ct.SoLuong < 0) errors.Add("Số lượng phải >= 0.");
            if (!string.IsNullOrWhiteSpace(ct.TrangThai) &&
                ct.TrangThai != "Còn hàng" && ct.TrangThai != "Hết hàng")
                errors.Add("Trạng thái không hợp lệ.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
