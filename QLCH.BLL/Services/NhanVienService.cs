// NhanVienService.cs
using QLCH.BLL.DTO;
using QLCH.BLL.Helpers;
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
        private readonly HopDongLaoDongRepository _hopDongRepo;
        private readonly BaoHiemRepository _baoHiemRepo;
        private readonly TaiKhoanService _taiKhoanRepo;

        public NhanVienService()
        {
            _nhanVienRepository = new NhanVienRepository();
            _hopDongRepo = new HopDongLaoDongRepository();
            _baoHiemRepo = new BaoHiemRepository();
            _taiKhoanRepo = new TaiKhoanService();
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
                ValidationHelper.Validate<NhanVien>(nhanVien);

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
                ValidationHelper.Validate<NhanVien>(nhanVien);

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

        public NhanVienDTO GetNhanVienFull(string maNV)
        {
            var nv = _nhanVienRepository.GetById(maNV);
            if (nv == null)
                throw new Exception("Không tìm thấy nhân viên.");

            var hd = _hopDongRepo.GetByMaNV(maNV);
            var bh = _baoHiemRepo.GetByMaNV(maNV);

            return new NhanVienDTO
            {
                // Nhân viên
                MaNV = nv.MaNV,
                HoTen = nv.HoTen,
                NgaySinh = nv.NgaySinh,
                GioiTinh = nv.GioiTinh,
                CMND = nv.CMND_CCCD,
                MaSoThue = nv.MaSoThue,
                SoDienThoai = nv.SoDienThoai,
                Email = nv.Email,
                DiaChi = nv.DiaChi,
                MaChucVu = nv.MaChucVu,
                MaPhongBan = nv.MaPhongBan,
                MaCuaHang = nv.MaCuaHang,
                LoaiHopDong = nv.LoaiHopDong,
                TrangThai = nv.TrangThai,
                NgayVaoLam = nv.NgayVaoLam,
                NgayNghiViec = nv.NgayNghiViec ?? DateTime.Now.AddYears(1),

                // Hợp đồng
                MaHopDong = hd?.MaHopDong,
                LuongCoBan = hd?.LuongCoBan,
                ThoiHanHD = hd?.ThoiHanHD,
                NgayKy = hd?.NgayKy ?? DateTime.Now,
                NgayHieuLuc = hd?.NgayHieuLuc ?? DateTime.Now,
                NgayKetThuc = hd?.NgayKetThuc ?? DateTime.Now,
                TrangThaiHopDong = hd?.TrangThai,

                // Bảo hiểm
                MaBH = bh?.MaBaoHiem,
                MaBHXH = bh?.SoBHXH,
                MaBHYT = bh?.SoBHYT,
                NhaCungCap = bh?.NhaCungCap,
                NgayCap = bh?.NgayCap ?? DateTime.Now,
                TrangThaiBaoHiem = bh?.TrangThai
            };
        }

        public bool AddNhanVienFull(NhanVien nv, HopDongLaoDong hd, BaoHiem bh)
        {
            ValidationHelper.Validate<NhanVien>(nv);
            ValidationHelper.Validate<HopDongLaoDong>(hd);
            ValidationHelper.Validate<BaoHiem>(bh);
            nv.MaNV = automaticGenerateMaNV();
            nv.CreatedAt = DateTime.Now;
            _nhanVienRepository.Add(nv);
            hd.MaNV = nv.MaNV;
            _hopDongRepo.Add(hd);
            bh.MaNV = nv.MaNV;
            _baoHiemRepo.Add(bh);
            // Tạo tài khoản cho nhân viên
            var tk = new TaiKhoan();
            tk.MaNV = nv.MaNV;
            tk.TenDangNhap = nv.MaNV;
            tk.MatKhau = nv.MaNV;
            tk.Email = nv.Email;
            tk.TrangThai = nv.TrangThai;
            _taiKhoanRepo.Add(tk);
            return true;
        }

        public bool UpdateNhanVienFull(NhanVien nv, HopDongLaoDong hd, BaoHiem bh)
        {
            ValidationHelper.Validate<NhanVien>(nv);
            ValidationHelper.Validate<HopDongLaoDong>(hd);
            ValidationHelper.Validate<BaoHiem>(bh);
            var existingNhanVien = _nhanVienRepository.GetById(nv.MaNV);
            if (existingNhanVien == null)
            {
                throw new Exception("Nhân viên không tồn tại");
            }
            nv.UpdatedAt = DateTime.Now;
            _nhanVienRepository.Update(nv);
            var existingHopDong = _hopDongRepo.GetByMaNV(nv.MaNV);
            if (existingHopDong != null)
            {
                hd.MaNV = nv.MaNV;
                _hopDongRepo.Update(hd);
            }
            else
            {
                hd.MaNV = nv.MaNV;
                _hopDongRepo.Add(hd);
            }
            var existingBaoHiem = _baoHiemRepo.GetByMaNV(nv.MaNV);
            if (existingBaoHiem != null)
            {
                bh.MaNV = nv.MaNV;
                _baoHiemRepo.Update(bh);
            }
            else
            {
                bh.MaNV = nv.MaNV;
                _baoHiemRepo.Add(bh);
            }
            return true;
        }
    }
}
