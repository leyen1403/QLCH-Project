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
    public class TinhLuongService : ITinhLuongService
    {
        private readonly TinhLuongRepository _repo = new TinhLuongRepository();

        public List<TinhLuong> GetAll()
        {
            return _repo.GetAll();
        }

        public TinhLuong GetById(int id)
        {
            return _repo.GetById(id);
        }

        public bool Add(TinhLuong t)
        {
            try
            {
                Validate(t);
                _repo.Add(t);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm bảng lương: {ex.Message}");
            }
        }

        public bool Update(TinhLuong t)
        {
            try
            {
                Validate(t);
                if (_repo.GetById(t.MaBangLuong) == null)
                    throw new Exception("Bảng lương không tồn tại.");
                _repo.Update(t);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật bảng lương: {ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Bảng lương không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa bảng lương: {ex.Message}");
            }
        }

        private void Validate(TinhLuong t)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(t.MaNV))
                errors.Add("Mã nhân viên không được để trống.");
            if (string.IsNullOrWhiteSpace(t.ThangNam) || !System.Text.RegularExpressions.Regex.IsMatch(t.ThangNam, @"^\d{4}-\d{2}$"))
                errors.Add("Tháng năm phải theo định dạng yyyy-MM.");
            if (t.LuongCoBan < 0)
                errors.Add("Lương cơ bản không được âm.");
            if (t.PhuCap < 0 || t.TienOT < 0 || t.TruPhat < 0 || t.Thuong < 0)
                errors.Add("Các khoản phụ cấp, OT, trừ phạt, thưởng không được âm.");

            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
