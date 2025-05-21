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
    public class ChiTietPhieuKiemKeService : IChiTietPhieuKiemKeService
    {
        private readonly ChiTietPhieuKiemKeRepository _repo = new ChiTietPhieuKiemKeRepository();
        private readonly ChiTietKhoRepository _khoRepo = new ChiTietKhoRepository();
        private readonly PhieuKiemKeRepository _kiemKeRepo = new PhieuKiemKeRepository();

        public List<ChiTietPhieuKiemKe> GetAll() => _repo.GetAll();

        public ChiTietPhieuKiemKe GetById(int id) => _repo.GetById(id);

        public bool Add(ChiTietPhieuKiemKe ct)
        {
            try
            {
                Validate(ct);

                // Lấy phiếu kiểm kê để biết mã kho
                var phieu = _kiemKeRepo.GetById(ct.MaPhieuKiemKe);
                if (phieu == null)
                    throw new Exception("Không tìm thấy phiếu kiểm kê.");

                int maKho = phieu.MaKho;

                // Lấy tồn kho hệ thống hiện tại
                int? soLuongTon = _khoRepo.GetSoLuongTonKho(maKho, ct.MaBienThe);

                if (!soLuongTon.HasValue)
                    throw new Exception("Không tìm thấy dữ liệu tồn kho hiện tại.");

                // Tính chênh lệch
                int chenhlech = ct.SoLuongThucTe - soLuongTon.Value;
                ct.SoLuongChenhLech = chenhlech;

                // Thêm chi tiết kiểm kê
                _repo.Add(ct);

                // Cập nhật lại tồn kho theo số thực tế
                _khoRepo.CapNhatTonKhoTheoKiemKe(maKho, ct.MaBienThe, ct.SoLuongThucTe);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi kiểm kê tồn kho: " + ex.Message);
            }
        }

        public bool Update(ChiTietPhieuKiemKe ct)
        {
            try
            {
                Validate(ct);
                if (_repo.GetById(ct.MaChiTietKK) == null)
                    throw new Exception("Chi tiết kiểm kê không tồn tại.");
                _repo.Update(ct);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật chi tiết kiểm kê: " + ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Chi tiết kiểm kê không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa chi tiết kiểm kê: " + ex.Message);
            }
        }

        private void Validate(ChiTietPhieuKiemKe ct)
        {
            var errors = new List<string>();
            if (ct.MaPhieuKiemKe <= 0)
                errors.Add("Mã phiếu kiểm kê không hợp lệ.");
            if (ct.MaBienThe <= 0)
                errors.Add("Mã biến thể không hợp lệ.");
            if (ct.SoLuongThucTe < 0)
                errors.Add("Số lượng thực tế không được âm.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
