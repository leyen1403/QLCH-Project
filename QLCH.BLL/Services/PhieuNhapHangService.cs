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
    public class PhieuNhapHangService : IPhieuNhapHangService
    {
        private readonly PhieuNhapHangRepository _repo = new PhieuNhapHangRepository();

        public List<PhieuNhapHang> GetAll() => _repo.GetAll();

        public PhieuNhapHang GetById(int id) => _repo.GetById(id);

        public bool Add(PhieuNhapHang ph)
        {
            try
            {
                Validate(ph);
                _repo.Add(ph);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm phiếu nhập: {ex.Message}");
            }
        }

        public bool Update(PhieuNhapHang ph)
        {
            try
            {
                Validate(ph);
                if (_repo.GetById(ph.MaPhieuNhap) == null)
                    throw new Exception("Phiếu nhập không tồn tại.");
                _repo.Update(ph);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật phiếu nhập: {ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Phiếu nhập không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa phiếu nhập: {ex.Message}");
            }
        }

        private void Validate(PhieuNhapHang ph)
        {
            var errors = new List<string>();
            if (ph.MaKho <= 0)
                errors.Add("Mã kho không hợp lệ.");
            if (string.IsNullOrWhiteSpace(ph.NguoiNhap))
                errors.Add("Người nhập không được để trống.");
            if (ph.TongTien < 0)
                errors.Add("Tổng tiền phải >= 0.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
