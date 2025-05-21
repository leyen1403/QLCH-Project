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
    public class KhachHangService : IKhachHangService
    {
        private readonly KhachHangRepository _repo = new KhachHangRepository();

        public List<KhachHang> GetAll() => _repo.GetAll();

        public KhachHang GetById(int id) => _repo.GetById(id);

        public bool Add(KhachHang kh)
        {
            try
            {
                Validate(kh);
                _repo.Add(kh);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm khách hàng: {ex.Message}");
            }
        }

        public bool Update(KhachHang kh)
        {
            try
            {
                Validate(kh);
                if (_repo.GetById(kh.MaKhachHang) == null)
                    throw new Exception("Khách hàng không tồn tại.");
                _repo.Update(kh);
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
                    throw new Exception("Khách hàng không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa: {ex.Message}");
            }
        }

        private void Validate(KhachHang kh)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(kh.HoTen))
                errors.Add("Họ tên không được để trống.");
            if (string.IsNullOrWhiteSpace(kh.SoDienThoai))
                errors.Add("Số điện thoại không được để trống.");
            if (!string.IsNullOrWhiteSpace(kh.LoaiKhachHang) &&
                kh.LoaiKhachHang != "Thành viên" &&
                kh.LoaiKhachHang != "VIP" &&
                kh.LoaiKhachHang != "Premium")
                errors.Add("Loại khách hàng không hợp lệ.");
            if (kh.DiemTichLuy < 0)
                errors.Add("Điểm tích lũy không được âm.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
