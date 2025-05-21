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
    public class ChiTietPhieuXuatKhoService : IChiTietPhieuXuatKhoService
    {
        private readonly ChiTietPhieuXuatKhoRepository _repo = new ChiTietPhieuXuatKhoRepository();
        private readonly ChiTietKhoRepository _khoRepo = new ChiTietKhoRepository();

        public List<ChiTietPhieuXuatKho> GetAll() => _repo.GetAll();

        public ChiTietPhieuXuatKho GetById(int id) => _repo.GetById(id);

        public bool Add(ChiTietPhieuXuatKho ct)
        {
            try
            {
                Validate(ct);
                _repo.Add(ct);

                // Lấy mã kho từ phiếu xuất
                var pxRepo = new PhieuXuatKhoRepository();
                var px = pxRepo.GetById(ct.MaPhieuXuat);
                if (px == null) throw new Exception("Không tìm thấy phiếu xuất kho.");

                // Trừ tồn kho
                _khoRepo.TruTonKho(px.MaKhoXuat, ct.MaBienThe, ct.SoLuong);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xuất hàng: " + ex.Message);
            }
        }

        public bool Update(ChiTietPhieuXuatKho ct)
        {
            try
            {
                Validate(ct);
                if (_repo.GetById(ct.MaChiTietXK) == null)
                    throw new Exception("Chi tiết phiếu xuất không tồn tại.");
                _repo.Update(ct);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật: " + ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Chi tiết phiếu xuất không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa: " + ex.Message);
            }
        }

        private void Validate(ChiTietPhieuXuatKho ct)
        {
            var errors = new List<string>();
            if (ct.MaPhieuXuat <= 0) errors.Add("Mã phiếu xuất không hợp lệ.");
            if (ct.MaBienThe <= 0) errors.Add("Mã biến thể không hợp lệ.");
            if (ct.SoLuong <= 0) errors.Add("Số lượng phải > 0.");
            if (ct.GiaXuat < 0) errors.Add("Giá xuất phải >= 0.");
            if (errors.Count > 0) throw new Exception(string.Join("\n", errors));
        }
    }
}
