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
    public class LichSuThangChucService : ILichSuThangChucService
    {
        private readonly LichSuThangChucRepository _repo = new LichSuThangChucRepository();

        public List<LichSuThangChuc> GetAll()
        {
            return _repo.GetAll();
        }

        public LichSuThangChuc GetById(int id)
        {
            return _repo.GetById(id);
        }

        public bool Add(LichSuThangChuc ls)
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
                throw new Exception($"Lỗi khi thêm lịch sử thăng chức: {ex.Message}");
            }
        }

        public bool Update(LichSuThangChuc ls)
        {
            try
            {
                Validate(ls);
                if (_repo.GetById(ls.MaLichSu) == null)
                    throw new Exception("Không tồn tại lịch sử thăng chức.");
                _repo.Update(ls);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật: {ex.Message}");
                throw new Exception($"Lỗi khi cập nhật lịch sử thăng chức: {ex.Message}");
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
                throw new Exception($"Lỗi khi xóa lịch sử thăng chức: {ex.Message}");
            }
        }

        private void Validate(LichSuThangChuc ls)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(ls.MaNV))
                errors.Add("Mã nhân viên không được để trống.");
            if (ls.MaChucVuMoi <= 0)
                errors.Add("Mã chức vụ mới không hợp lệ.");
            if (ls.NgayThangChuc == default)
                errors.Add("Ngày thăng chức không được để trống.");

            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
