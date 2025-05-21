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
    public class PhieuXuatKhoService : IPhieuXuatKhoService
    {
        private readonly PhieuXuatKhoRepository _repo = new PhieuXuatKhoRepository();

        public List<PhieuXuatKho> GetAll() => _repo.GetAll();

        public PhieuXuatKho GetById(int id) => _repo.GetById(id);

        public bool Add(PhieuXuatKho px)
        {
            try
            {
                Validate(px);
                _repo.Add(px);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm phiếu xuất kho: " + ex.Message);
            }
        }

        public bool Update(PhieuXuatKho px)
        {
            try
            {
                Validate(px);
                if (_repo.GetById(px.MaPhieuXuat) == null)
                    throw new Exception("Phiếu xuất không tồn tại.");
                _repo.Update(px);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật phiếu xuất kho: " + ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Phiếu xuất không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa phiếu xuất kho: " + ex.Message);
            }
        }

        private void Validate(PhieuXuatKho px)
        {
            var errors = new List<string>();
            if (px.MaKhoXuat <= 0)
                errors.Add("Mã kho xuất không hợp lệ.");
            if (string.IsNullOrWhiteSpace(px.LoaiXuat) ||
               (px.LoaiXuat != "Chuyển kho" && px.LoaiXuat != "Trả hàng"))
                errors.Add("Loại xuất không hợp lệ.");
            if (px.TongTien < 0)
                errors.Add("Tổng tiền không được âm.");
            if (px.NgayXuat > DateTime.Now.AddDays(1))
                errors.Add("Ngày xuất không hợp lệ.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
