using QLCH.BLL.Interfaces;
using QLCH.DAL;
using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Services
{
    public class ManHinhService : IManHinhService
    {
        private readonly ManHinhRepository _manHinhRepository;

        public ManHinhService()
        {
            _manHinhRepository = new ManHinhRepository();
        }

        // 🔹 Thêm mới màn hình
        public bool Add(ManHinh manHinh)
        {
            ValidateManHinh(manHinh);

            // Thiết lập thông tin mặc định
            manHinh.ThoiGianTao = DateTime.Now;
            manHinh.ThoiGianCapNhat = DateTime.Now;
            manHinh.TrangThai = true;

            // Gọi Repository để thêm vào database
            return _manHinhRepository.Add(manHinh);
        }

        // 🔹 Lấy danh sách toàn bộ màn hình
        public List<ManHinh> GetAll()
        {
            return _manHinhRepository.GetAll();
        }

        // 🔹 Lấy thông tin màn hình theo mã
        public ManHinh GetById(string maMH)
        {
            if (string.IsNullOrEmpty(maMH))
            {
                throw new ArgumentException("Mã màn hình không được để trống");
            }

            var manHinh = _manHinhRepository.GetById(maMH);

            if (manHinh == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy màn hình với mã: {maMH}");
            }

            return manHinh;
        }

        // 🔹 Cập nhật thông tin màn hình
        public bool Update(ManHinh manHinh)
        {
            ValidateManHinh(manHinh);

            var existingManHinh = _manHinhRepository.GetById(manHinh.MaMH);
            if (existingManHinh == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy màn hình với mã: {manHinh.MaMH}");
            }

            manHinh.ThoiGianCapNhat = DateTime.Now;

            return _manHinhRepository.Update(manHinh);
        }

        // 🔹 Xóa màn hình
        public bool Delete(string maMH)
        {
            if (string.IsNullOrEmpty(maMH))
            {
                throw new ArgumentException("Mã màn hình không được để trống");
            }

            var existingManHinh = _manHinhRepository.GetById(maMH);
            if (existingManHinh == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy màn hình với mã: {maMH}");
            }

            return _manHinhRepository.Delete(maMH);
        }

        // 🔹 Kiểm tra thông tin màn hình
        private void ValidateManHinh(ManHinh manHinh)
        {
            if (manHinh == null)
                throw new ArgumentNullException(nameof(manHinh), "Đối tượng màn hình không được null");

            if (string.IsNullOrEmpty(manHinh.MaMH))
                throw new ArgumentException("Mã màn hình không được để trống");

            if (string.IsNullOrEmpty(manHinh.TenMH))
                throw new ArgumentException("Tên màn hình không được để trống");

        }
    }
}
