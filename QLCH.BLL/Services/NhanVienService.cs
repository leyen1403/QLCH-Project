// NhanVienService.cs
using QLCH.BLL.Interfaces;
using QLCH.DAL.Models;
using QLCH.DAL.Repositorys;
using System;
using System.Collections.Generic;

namespace QLCH.BLL.Services
{
    public class NhanVienService : INhanVienService
    {
        private readonly NhanVienRepository _nhanVienRepository;

        public NhanVienService()
        {
            _nhanVienRepository = new NhanVienRepository();
        }

        public List<NhanVien> GetAllNhanViens()
        {
            return _nhanVienRepository.GetAll();
        }

        public NhanVien GetNhanVienById(string id)
        {
            return _nhanVienRepository.GetById(id);
        }

        public bool AddNhanVien(NhanVien nhanVien)
        {
            try
            {
                // Validate thông tin nhân viên
                ValidateNhanVien(nhanVien);

                // Tạo mã nhân viên tự động
                nhanVien.MaNV = automaticGenerateMaNV();
                nhanVien.CreatedAt = DateTime.Now;

                // Thực hiện thêm vào CSDL
                _nhanVienRepository.Add(nhanVien);
                Console.WriteLine("Thêm nhân viên thành công");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm nhân viên: {ex.Message}");
                throw new Exception($"Lỗi khi thêm nhân viên: {ex.Message}");
            }
        }

        public bool UpdateNhanVien(NhanVien nhanVien)
        {
            try
            {
                // Validate thông tin nhân viên
                ValidateNhanVien(nhanVien);

                // Kiểm tra tồn tại
                var existingNhanVien = _nhanVienRepository.GetById(nhanVien.MaNV);
                if (existingNhanVien == null)
                {
                    throw new Exception("Nhân viên không tồn tại");
                }

                // Cập nhật thông tin
                nhanVien.UpdatedAt = DateTime.Now;
                _nhanVienRepository.Update(nhanVien);

                Console.WriteLine("Cập nhật thông tin nhân viên thành công");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật nhân viên: {ex.Message}");
                throw new Exception($"Lỗi khi cập nhật nhân viên: {ex.Message}");
            }
        }

        public bool DeleteNhanVien(string id)
        {
            try
            {
                // Kiểm tra tồn tại
                var existingNhanVien = _nhanVienRepository.GetById(id);
                if (existingNhanVien == null)
                {
                    throw new Exception("Nhân viên không tồn tại");
                }

                // Thực hiện xóa
                _nhanVienRepository.Delete(id);
                Console.WriteLine("Xóa nhân viên thành công");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xóa nhân viên: {ex.Message}");
                throw new Exception($"Lỗi khi xóa nhân viên: {ex.Message}");
            }
        }

        private string automaticGenerateMaNV()
        {
            try
            {
                string lastMaNV = _nhanVienRepository.GetLastNhanVien()?.MaNV;
                if (string.IsNullOrEmpty(lastMaNV))
                {
                    return "NV0001";
                }

                int maSo = int.Parse(lastMaNV.Substring(2)) + 1;
                return "NV" + maSo.ToString("D4");
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tự động tạo mã nhân viên: {ex.Message}");
            }
        }

        private void ValidateNhanVien(NhanVien nv)
        {
            if (nv == null)
            {
                throw new Exception("Đối tượng nhân viên không được null");
            }

            var errors = new List<string>();

            var requiredStrings = new Dictionary<string, string>
            {
                { "HoTen", nv.HoTen },
                { "CMND_CCCD", nv.CMND_CCCD },
                { "SoDienThoai", nv.SoDienThoai },
                { "Email", nv.Email },
                { "LoaiHopDong", nv.LoaiHopDong },
                { "TrangThai", nv.TrangThai }
            };

            foreach (var item in requiredStrings)
            {
                if (string.IsNullOrEmpty(item.Value))
                {
                    errors.Add($"{item.Key} không được để trống");
                }
            }

            if (nv.MaChucVu <= 0) errors.Add("Mã chức vụ không hợp lệ");
            if (nv.MaPhongBan <= 0) errors.Add("Mã phòng ban không hợp lệ");
            if (nv.MaCuaHang <= 0) errors.Add("Mã cửa hàng không hợp lệ");

            if (nv.NgayVaoLam == DateTime.MinValue) errors.Add("Ngày vào làm không được để trống");
            if (nv.NgaySinh == DateTime.MinValue) errors.Add("Ngày sinh không được để trống");

            if (errors.Count > 0)
            {
                throw new Exception(string.Join("\n", errors));
            }
        }
    }
}
