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
    public class TaiKhoanService : ITaiKhoan
    {
        private readonly TaiKhoanRepository _taiKhoanRepository;

        public TaiKhoanService()
        {
            _taiKhoanRepository = new TaiKhoanRepository();
        }

        // Thêm mới tài khoản
        public void Add(TaiKhoan taiKhoan)
        {
            ValidateTaiKhoan(taiKhoan);

            // Thiết lập giá trị mặc định
            taiKhoan.ThoiGianTao = DateTime.Now;
            taiKhoan.ThoiGianCapNhat = DateTime.Now;
            taiKhoan.TrangThai = true;

            // Kiểm tra sự tồn tại
            var existingTaiKhoan = _taiKhoanRepository.GetByID(taiKhoan.MaTK);
            if (existingTaiKhoan != null)
            {
                throw new InvalidOperationException($"Mã tài khoản '{taiKhoan.MaTK}' đã tồn tại.");
            }

            // Gọi Repository để thêm vào database
            _taiKhoanRepository.Add(taiKhoan);
        }

        // Cập nhật thông tin tài khoản
        public void Update(TaiKhoan taiKhoan)
        {
            ValidateTaiKhoan(taiKhoan);

            // Kiểm tra sự tồn tại
            var existingTaiKhoan = _taiKhoanRepository.GetByID(taiKhoan.MaTK);
            if (existingTaiKhoan == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy tài khoản với mã: {taiKhoan.MaTK}");
            }

            // Cập nhật thời gian
            taiKhoan.ThoiGianCapNhat = DateTime.Now;

            // Thực hiện cập nhật vào database
            _taiKhoanRepository.Update(taiKhoan);
        }

        // Xóa tài khoản
        public void Delete(string maTK)
        {
            if (string.IsNullOrEmpty(maTK))
            {
                throw new ArgumentException("Mã tài khoản không được để trống");
            }

            var existingTaiKhoan = _taiKhoanRepository.GetByID(maTK);
            if (existingTaiKhoan == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy tài khoản với mã: {maTK}");
            }

            _taiKhoanRepository.Delete(maTK);
        }

        // Lấy thông tin tài khoản theo mã
        public TaiKhoan GetByID(string maTK)
        {
            if (string.IsNullOrEmpty(maTK))
            {
                throw new ArgumentException("Mã tài khoản không được để trống");
            }

            var taiKhoan = _taiKhoanRepository.GetByID(maTK);

            if (taiKhoan == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy tài khoản với mã: {maTK}");
            }

            return taiKhoan;
        }

        // Lấy danh sách toàn bộ tài khoản
        public List<TaiKhoan> GetAll()
        {
            var taiKhoans = _taiKhoanRepository.GetAll();
            return taiKhoans ?? new List<TaiKhoan>();
        }

        // Kiểm tra thông tin tài khoản trước khi thêm/sửa
        private void ValidateTaiKhoan(TaiKhoan taiKhoan)
        {
            if (taiKhoan == null)
                throw new ArgumentNullException(nameof(taiKhoan), "Đối tượng tài khoản không được null");

            var errors = new List<string>();

            if (string.IsNullOrEmpty(taiKhoan.MaTK))
                errors.Add("Mã tài khoản không được để trống");

            if (string.IsNullOrEmpty(taiKhoan.MaNV))
                errors.Add("Mã nhân viên không được để trống");

            if (string.IsNullOrEmpty(taiKhoan.TenDangNhap))
                errors.Add("Tên đăng nhập không được để trống");

            if (string.IsNullOrEmpty(taiKhoan.MatKhau))
                errors.Add("Mật khẩu không được để trống");

            if (errors.Count > 0)
            {
                throw new ArgumentException(string.Join("\n", errors));
            }
        }
        
        public TaiKhoan Login(string username, string password)
        {
            // Kiểm tra thông tin đầu vào
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Tên đăng nhập và mật khẩu không được để trống.");
            }

            // Gọi đến Repository để lấy thông tin tài khoản
            var taiKhoan = _taiKhoanRepository.GetByUsername(username);

            // Kiểm tra sự tồn tại của tài khoản
            if (taiKhoan == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy tài khoản với tên đăng nhập: {username}");
            }

            // Kiểm tra mật khẩu
            if (taiKhoan.MatKhau != password)
            {
                throw new UnauthorizedAccessException("Mật khẩu không chính xác.");
            }

            // Nếu đúng, trả về thông tin tài khoản
            return taiKhoan;
        }
    }
}
