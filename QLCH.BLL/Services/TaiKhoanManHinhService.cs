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
    public class TaiKhoanManHinhService : ITaiKhoanManHinh
    {
        private readonly TaiKhoanManHinhRepository _taiKhoanManHinhRepository;

        public TaiKhoanManHinhService()
        {
            _taiKhoanManHinhRepository = new TaiKhoanManHinhRepository();
        }

        // 🔹 Thêm mới Tài Khoản - Màn Hình
        public void Add(TaiKhoanManHinh taiKhoanManHinh)
        {
            ValidateTaiKhoanManHinh(taiKhoanManHinh);

            // Thiết lập thông tin mặc định
            taiKhoanManHinh.ThoiGianTao = DateTime.Now;
            taiKhoanManHinh.ThoiGianCapNhat = DateTime.Now;
            taiKhoanManHinh.TrangThai = true;

            // Kiểm tra sự tồn tại trước khi thêm
            var existing = _taiKhoanManHinhRepository.GetByID(taiKhoanManHinh.MaTK, taiKhoanManHinh.MaMH);
            if (existing != null)
            {
                throw new InvalidOperationException($"Quan hệ giữa tài khoản '{taiKhoanManHinh.MaTK}' và màn hình '{taiKhoanManHinh.MaMH}' đã tồn tại.");
            }

            // Thêm mới vào database
            _taiKhoanManHinhRepository.Add(taiKhoanManHinh);
        }

        // 🔹 Cập nhật thông tin Tài Khoản - Màn Hình
        public void Update(TaiKhoanManHinh taiKhoanManHinh)
        {
            ValidateTaiKhoanManHinh(taiKhoanManHinh);

            // Kiểm tra sự tồn tại
            var existing = _taiKhoanManHinhRepository.GetByID(taiKhoanManHinh.MaTK, taiKhoanManHinh.MaMH);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy tài khoản '{taiKhoanManHinh.MaTK}' và màn hình '{taiKhoanManHinh.MaMH}'.");
            }

            // Cập nhật thời gian sửa đổi
            taiKhoanManHinh.ThoiGianCapNhat = DateTime.Now;

            // Thực hiện cập nhật vào database
            _taiKhoanManHinhRepository.Update(taiKhoanManHinh);
        }

        // 🔹 Xóa Tài Khoản - Màn Hình
        public void Delete(string maTK, string maMH)
        {
            if (string.IsNullOrEmpty(maTK) || string.IsNullOrEmpty(maMH))
            {
                throw new ArgumentException("Mã tài khoản hoặc mã màn hình không được để trống.");
            }

            var existing = _taiKhoanManHinhRepository.GetByID(maTK, maMH);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy tài khoản '{maTK}' và màn hình '{maMH}'.");
            }

            _taiKhoanManHinhRepository.Delete(maTK, maMH);
        }

        // 🔹 Lấy thông tin theo mã
        public TaiKhoanManHinh GetByID(string maTK, string maMH)
        {
            if (string.IsNullOrEmpty(maTK) || string.IsNullOrEmpty(maMH))
            {
                throw new ArgumentException("Mã tài khoản và mã màn hình không được để trống.");
            }

            var taiKhoanManHinh = _taiKhoanManHinhRepository.GetByID(maTK, maMH);

            if (taiKhoanManHinh == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy tài khoản '{maTK}' và màn hình '{maMH}'.");
            }

            return taiKhoanManHinh;
        }

        // 🔹 Lấy toàn bộ danh sách
        public List<TaiKhoanManHinh> GetAll()
        {
            var list = _taiKhoanManHinhRepository.GetAll();
            return list ?? new List<TaiKhoanManHinh>();
        }

        // 🔹 Kiểm tra thông tin tài khoản - màn hình trước khi thêm/sửa
        private void ValidateTaiKhoanManHinh(TaiKhoanManHinh taiKhoanManHinh)
        {
            if (taiKhoanManHinh == null)
                throw new ArgumentNullException(nameof(taiKhoanManHinh), "Đối tượng không được null");

            var errors = new List<string>();

            if (string.IsNullOrEmpty(taiKhoanManHinh.MaTK))
                errors.Add("Mã tài khoản không được để trống.");

            if (string.IsNullOrEmpty(taiKhoanManHinh.MaMH))
                errors.Add("Mã màn hình không được để trống.");

            if (errors.Count > 0)
            {
                throw new ArgumentException(string.Join("\n", errors));
            }
        }
    }
}
