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
        private readonly TaiKhoanRepository _taiKhoanRepository;
        private readonly TaiKhoanManHinhRepository _taiKhoanManHinhRepository;

        public NhanVienService()
        {
            _nhanVienRepository = new NhanVienRepository();
            _taiKhoanRepository = new TaiKhoanRepository();
            _taiKhoanManHinhRepository = new TaiKhoanManHinhRepository();
        }
        public void Add(NhanVien nhanVien)
        {
            // Sinh mã nhân viên tự động
            string maNV = AutomaticGenerateMaNV();
            nhanVien.MaNV = maNV;

            // 2Validate thông tin
            ValidateNhanVien(nhanVien);

            // Thiết lập thông tin mặc định
            nhanVien.ThoiGianTao = DateTime.Now;
            nhanVien.ThoiGianCapNhat = DateTime.Now;
            nhanVien.TrangThai = true;

            // 4Thêm vào database
            _nhanVienRepository.Add(nhanVien);

            // 5Tạo tài khoản cho nhân viên
            TaiKhoan taiKhoan = new TaiKhoan
            {
                MaTK = "TK_" + maNV,
                MaNV = maNV,
                TenDangNhap = nhanVien.SDT,
                MatKhau = nhanVien.SDT, // Mật khẩu mặc định là số điện thoại
                ThoiGianTao = DateTime.Now,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = true
            };
            _taiKhoanRepository.Add(taiKhoan);         

            Console.WriteLine("Thêm nhân viên và tài khoản thành công!");
        }

        private string AutomaticGenerateMaNV()
        {
            // Lấy mã cuối cùng từ database
            string lastMaNV = _nhanVienRepository.GetLastNV();

            // Nếu chưa có khách hàng nào, bắt đầu từ NV0001
            if (string.IsNullOrEmpty(lastMaNV))
            {
                return "NV0001";
            }

            // Tách phần số từ "NV0001" => "0001"
            string numberPart = lastMaNV.Substring(2);

            // Thử chuyển đổi thành số, nếu thất bại thì trả về NV00001
            if (int.TryParse(numberPart, out int lastNumber))
            {
                int newNumber = lastNumber + 1;
                return "NV" + newNumber.ToString("D4");
            }
            else
            {
                // Nếu lỗi format, quay lại mã mặc định
                return "NV000001";
            }
        }

        public void Delete(string maNV)
        {
            if (string.IsNullOrEmpty(maNV))
            {
                Console.WriteLine("Mã nhân viên không được để trống");
                throw new ArgumentException("Mã nhân viên không được để trống");
            }

            var nhanVien = _nhanVienRepository.GetByID(maNV);

            if (nhanVien == null)
            {
                Console.WriteLine($"Không tìm thấy nhân viên với mã: {maNV}");
                throw new KeyNotFoundException($"Không tìm thấy nhân viên với mã: {maNV}");
            }

            _nhanVienRepository.Delete(maNV);
        }

        public List<NhanVien> GetAll()
        {
            var nhanViens = _nhanVienRepository.GetAll();
            return nhanViens ?? new List<NhanVien>();
        }

        public NhanVien GetByID(string maNV)
        {
            if (string.IsNullOrEmpty(maNV))
            {
                Console.WriteLine("Mã nhân viên không được để trống");
                throw new ArgumentException("Mã nhân viên không được để trống");
            }

            var nhanVien = _nhanVienRepository.GetByID(maNV);

            if (nhanVien == null)
            {
                Console.WriteLine($"Không tìm thấy nhân viên với mã: {maNV}");
                throw new KeyNotFoundException($"Không tìm thấy nhân viên với mã: {maNV}");
            }

            return nhanVien;
        }

        public void Update(NhanVien nhanVien)
        {
            ValidateNhanVien(nhanVien);

            // Kiểm tra sự tồn tại của nhân viên
            var existingNhanVien = _nhanVienRepository.GetByID(nhanVien.MaNV);
            if (existingNhanVien == null)
            {
                Console.WriteLine($"Không tìm thấy nhân viên với mã: {nhanVien.MaNV}");
                throw new KeyNotFoundException($"Không tìm thấy nhân viên với mã: {nhanVien.MaNV}");
            }

            // Cập nhật thời gian
            nhanVien.ThoiGianCapNhat = DateTime.Now;

            // Thực hiện cập nhật
            _nhanVienRepository.Update(nhanVien);
        }
        // Kiểm tra thông tin nhân viên
        private void ValidateNhanVien(NhanVien nhanVien)
        {
            if (nhanVien == null)
                throw new ArgumentNullException(nameof(nhanVien), "Đối tượng nhân viên không được null");

            var errors = new List<string>();

            if (string.IsNullOrEmpty(nhanVien.MaNV))
                errors.Add("Mã nhân viên không được để trống");

            if (string.IsNullOrEmpty(nhanVien.TenNV))
                errors.Add("Tên nhân viên không được để trống");

            if (string.IsNullOrEmpty(nhanVien.SDT))
                errors.Add("Số điện thoại không được để trống");

            if (string.IsNullOrEmpty(nhanVien.Email))
                errors.Add("Email không được để trống");

            if (string.IsNullOrEmpty(nhanVien.ChucVu))
                errors.Add("Chức vụ không được để trống");

            if (nhanVien.MucLuong < 0)
                errors.Add("Mức lương không được âm");

            if (errors.Count > 0)
            {
                Console.WriteLine(string.Join("\n", errors));
                throw new ArgumentException(string.Join("\n", errors));
            }
        }
    }
}
