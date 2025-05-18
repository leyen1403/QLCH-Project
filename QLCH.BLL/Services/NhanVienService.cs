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
    public class NhanVienService : INhanVienService
    {
        private readonly NhanVienRepository _nhanVienRepository;

        public NhanVienService()
        {
            _nhanVienRepository = new NhanVienRepository();
        }

        public List<NhanVien> GetAllNhanViens()
        {
            if (_nhanVienRepository.GetAll().Count <= 0)
            {
                throw new Exception("Không có nhân viên nào trong hệ thống");
            }
            return _nhanVienRepository.GetAll();
        }

        public NhanVien GetNhanVienById(string id)
        {
            if (_nhanVienRepository.GetById(id) == null)
            {
                throw new Exception("Nhân viên không tồn tại");
            }
            return _nhanVienRepository.GetById(id);
        }

        public bool AddNhanVien(NhanVien nhanVien)
        {
            try
            {
                string maNV = automaticGenerateMaNV();
                _nhanVienRepository.Add(nhanVien);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm nhân viên: {ex.Message}");
                return false;
            }
        }

        private string automaticGenerateMaNV()
        {
            try
            {
                string maNV = _nhanVienRepository.GetLastNhanVien().MaNV;
                if (maNV == null)
                {
                    return "NV0001";
                }
                else
                {
                    int maSo = int.Parse(maNV.Substring(2)) + 1;
                    return "NV" + maSo.ToString("D4");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tự động tạo mã nhân viên: {ex.Message}");
            }
        }

        public bool UpdateNhanVien(NhanVien nhanVien)
        {
            try
            {
                ValidateNhanVien(nhanVien);
                var existingNhanVien = GetNhanVienById(nhanVien.MaNV);
                if(existingNhanVien == null)
                {
                    throw new Exception("Nhân viên không tồn tại");
                }
                nhanVien.UpdatedAt = DateTime.Now;
                _nhanVienRepository.Update(nhanVien);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật nhân viên: {ex.Message}");
                return false;
            }
        }

        public bool DeleteNhanVien(string id)
        {
            try
            {
                _nhanVienRepository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xóa nhân viên: {ex.Message}");
                return false;
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
                { "MaNV", nv.MaNV },
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

            if (nv.MaChucVu <= 0)
            {
                errors.Add("Mã chức vụ không hợp lệ");
            }
            if (nv.MaPhongBan <= 0)
            {
                errors.Add("Mã phòng ban không hợp lệ");
            }
            if (nv.MaCuaHang <= 0)
            {
                errors.Add("Mã cửa hàng không hợp lệ");
            }

            if (nv.NgayVaoLam == DateTime.MinValue)
            {
                errors.Add("Ngày vào làm không được để trống");
            }

            if (nv.NgaySinh == DateTime.MinValue)
            {
                errors.Add("Ngày sinh không được để trống");
            }

            if (errors.Count > 0)
            {
                throw new Exception(string.Join("\n", errors));
            }
        }

    }
}
