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
    public class ChiTietPhieuNhapService : IChiTietPhieuNhapService
    {
        private readonly ChiTietPhieuNhapRepository _repo = new ChiTietPhieuNhapRepository();

        public List<ChiTietPhieuNhap> GetAll() => _repo.GetAll();

        public ChiTietPhieuNhap GetById(int id) => _repo.GetById(id);

        public bool Add(ChiTietPhieuNhap ct)
        {
            try
            {
                Validate(ct);
                _repo.Add(ct);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm chi tiết phiếu nhập: {ex.Message}");
            }
        }

        public bool Update(ChiTietPhieuNhap ct)
        {
            try
            {
                Validate(ct);
                if (_repo.GetById(ct.MaChiTiet) == null)
                    throw new Exception("Chi tiết không tồn tại.");
                _repo.Update(ct);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật chi tiết phiếu nhập: {ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Chi tiết không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa chi tiết phiếu nhập: {ex.Message}");
            }
        }

        private void Validate(ChiTietPhieuNhap ct)
        {
            var errors = new List<string>();
            if (ct.MaPhieuNhap <= 0)
                errors.Add("Mã phiếu nhập không hợp lệ.");
            if (ct.MaBienThe <= 0)
                errors.Add("Mã biến thể không hợp lệ.");
            if (ct.SoLuongNhap <= 0)
                errors.Add("Số lượng nhập phải > 0.");
            if (ct.DonGiaNhap < 0)
                errors.Add("Đơn giá nhập phải >= 0.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
