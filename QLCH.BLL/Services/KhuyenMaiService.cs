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
    public class KhuyenMaiService : IKhuyenMaiService
    {
        private readonly KhuyenMaiRepository _repo = new KhuyenMaiRepository();

        public List<KhuyenMai> GetAll() => _repo.GetAll();

        public KhuyenMai GetById(int id) => _repo.GetById(id);

        public bool Add(KhuyenMai km)
        {
            try
            {
                Validate(km);
                _repo.Add(km);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm khuyến mãi: " + ex.Message);
            }
        }

        public bool Update(KhuyenMai km)
        {
            try
            {
                Validate(km);
                if (_repo.GetById(km.MaKhuyenMai) == null)
                    throw new Exception("Khuyến mãi không tồn tại.");
                _repo.Update(km);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật khuyến mãi: " + ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Khuyến mãi không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa khuyến mãi: " + ex.Message);
            }
        }

        private void Validate(KhuyenMai km)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(km.TenKhuyenMai)) errors.Add("Tên khuyến mãi không được để trống.");
            if (km.GiaTri <= 0) errors.Add("Giá trị khuyến mãi phải lớn hơn 0.");
            if (string.IsNullOrWhiteSpace(km.LoaiGiamGia) ||
               (km.LoaiGiamGia != "Phần trăm" && km.LoaiGiamGia != "Số tiền"))
                errors.Add("Loại giảm giá không hợp lệ.");
            if (km.NgayKetThuc < km.NgayBatDau)
                errors.Add("Ngày kết thúc không được nhỏ hơn ngày bắt đầu.");
            if (!string.IsNullOrWhiteSpace(km.TrangThai) &&
               (km.TrangThai != "Hoạt động" && km.TrangThai != "Ngừng hoạt động"))
                errors.Add("Trạng thái không hợp lệ.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
