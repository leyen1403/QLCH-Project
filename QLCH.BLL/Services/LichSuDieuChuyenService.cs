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
    public class LichSuDieuChuyenService : ILichSuDieuChuyenService
    {
        private readonly LichSuDieuChuyenRepository _repo = new LichSuDieuChuyenRepository();

        public List<LichSuDieuChuyen> GetAll()
        {
            return _repo.GetAll();
        }

        public LichSuDieuChuyen GetById(int id)
        {
            return _repo.GetById(id);
        }

        public bool Add(LichSuDieuChuyen ls)
        {
            try
            {
                Validate(ls);
                _repo.Add(ls);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm: {ex.Message}");
                throw new Exception($"Lỗi khi thêm: {ex.Message}");
            }
        }

        public bool Update(LichSuDieuChuyen ls)
        {
            try
            {
                Validate(ls);
                if (_repo.GetById(ls.MaDieuChuyen) == null)
                    throw new Exception("Không tồn tại bản ghi.");
                _repo.Update(ls);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật: {ex.Message}");
                throw new Exception($"Lỗi khi cập nhật: {ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Không tồn tại bản ghi.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xóa: {ex.Message}");
                throw new Exception($"Lỗi khi xóa: {ex.Message}");
            }
        }

        private void Validate(LichSuDieuChuyen ls)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(ls.MaNV))
                errors.Add("Mã nhân viên không được để trống.");
            if (ls.MaPhongBanMoi <= 0)
                errors.Add("Phòng ban mới không hợp lệ.");
            if (ls.MaCuaHangMoi <= 0)
                errors.Add("Cửa hàng mới không hợp lệ.");
            if (ls.NgayDieuChuyen == default)
                errors.Add("Ngày điều chuyển không hợp lệ.");

            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
