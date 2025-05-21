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
    public class PhieuThanhToanService : IPhieuThanhToanService
    {
        private readonly PhieuThanhToanRepository _repo = new PhieuThanhToanRepository();
        private readonly DonHangRepository _donHangRepo = new DonHangRepository();

        public List<PhieuThanhToan> GetAll() => _repo.GetAll();

        public PhieuThanhToan GetById(int id) => _repo.GetById(id);

        public bool Add(PhieuThanhToan pt)
        {
            try
            {
                Validate(pt);
                _repo.Add(pt);

                // Cập nhật trạng thái đơn hàng sang "Đã thanh toán"
                _donHangRepo.UpdateTrangThai(pt.MaDonHang, "Đã thanh toán");

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm phiếu thanh toán: " + ex.Message);
            }
        }


        public bool Update(PhieuThanhToan pt)
        {
            try
            {
                Validate(pt);
                if (_repo.GetById(pt.MaPhieuThanhToan) == null)
                    throw new Exception("Phiếu thanh toán không tồn tại.");
                _repo.Update(pt);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật phiếu thanh toán: " + ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Phiếu thanh toán không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa phiếu thanh toán: " + ex.Message);
            }
        }

        private void Validate(PhieuThanhToan pt)
        {
            var errors = new List<string>();
            if (pt.MaDonHang <= 0)
                errors.Add("Mã đơn hàng không hợp lệ.");
            if (pt.SoTienThanhToan < 0)
                errors.Add("Số tiền thanh toán không được âm.");
            if (string.IsNullOrWhiteSpace(pt.PhuongThuc) ||
               (pt.PhuongThuc != "Tiền mặt" && pt.PhuongThuc != "Chuyển khoản" && pt.PhuongThuc != "Thẻ tín dụng"))
                errors.Add("Phương thức thanh toán không hợp lệ.");
            if (!string.IsNullOrEmpty(pt.TrangThai) &&
               pt.TrangThai != "Đã thanh toán" && pt.TrangThai != "Hủy")
                errors.Add("Trạng thái không hợp lệ.");

            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }

        public bool HuyPhieuThanhToan(int id)
        {
            try
            {
                var pt = _repo.GetById(id);
                if (pt == null)
                    throw new Exception("Phiếu thanh toán không tồn tại.");

                if (pt.TrangThai == "Hủy")
                    throw new Exception("Phiếu thanh toán đã bị hủy trước đó.");

                // Cập nhật trạng thái phiếu
                pt.TrangThai = "Hủy";
                _repo.Update(pt);

                // Cập nhật trạng thái đơn hàng về "Chưa thanh toán"
                _donHangRepo.UpdateTrangThai(pt.MaDonHang, "Chưa thanh toán");

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi hủy phiếu thanh toán: " + ex.Message);
            }
        }

    }
}
