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
    public class DonHangService : IDonHangService
    {
        private readonly DonHangRepository _repo = new DonHangRepository();

        public List<DonHang> GetAll() => _repo.GetAll();

        public DonHang GetById(int id) => _repo.GetById(id);

        public bool Add(DonHang dh)
        {
            try
            {
                Validate(dh);
                _repo.Add(dh);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm đơn hàng: {ex.Message}");
            }
        }

        public bool Update(DonHang dh)
        {
            try
            {
                Validate(dh);
                if (_repo.GetById(dh.MaDonHang) == null)
                    throw new Exception("Đơn hàng không tồn tại.");
                _repo.Update(dh);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật đơn hàng: {ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Đơn hàng không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa đơn hàng: {ex.Message}");
            }
        }

        private void Validate(DonHang dh)
        {
            var errors = new List<string>();
            if (dh.TongTien < 0)
                errors.Add("Tổng tiền phải >= 0.");
            if (dh.GiamGia < 0)
                errors.Add("Giảm giá phải >= 0.");
            if (!string.IsNullOrWhiteSpace(dh.TrangThai) &&
                dh.TrangThai != "Chưa thanh toán" &&
                dh.TrangThai != "Đã thanh toán" &&
                dh.TrangThai != "Hủy")
                errors.Add("Trạng thái đơn hàng không hợp lệ.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
