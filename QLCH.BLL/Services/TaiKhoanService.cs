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
    public class TaiKhoanService : ITaiKhoanService
    {
        private readonly TaiKhoanRepository _repo = new TaiKhoanRepository();

        public List<TaiKhoan> GetAll() => _repo.GetAll();

        public TaiKhoan GetById(int id) => _repo.GetById(id);

        private string HashPassword(string password)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hashBytes = sha.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public bool Add(TaiKhoan tk)
        {
            try
            {
                Validate(tk);
                tk.MatKhau = HashPassword(tk.MatKhau);
                _repo.Add(tk);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm tài khoản: {ex.Message}");
            }
        }


        public bool Update(TaiKhoan tk)
        {
            try
            {
                Validate(tk);
                if (_repo.GetById(tk.MaTaiKhoan) == null)
                    throw new Exception("Tài khoản không tồn tại.");
                _repo.Update(tk);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật tài khoản: {ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Tài khoản không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa tài khoản: {ex.Message}");
            }
        }

        private void Validate(TaiKhoan tk)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(tk.MaNV))
                errors.Add("Mã nhân viên không được để trống.");
            if (string.IsNullOrWhiteSpace(tk.TenDangNhap))
                errors.Add("Tên đăng nhập không được để trống.");
            if (string.IsNullOrWhiteSpace(tk.MatKhau))
                errors.Add("Mật khẩu không được để trống.");
            if (string.IsNullOrWhiteSpace(tk.Email))
                errors.Add("Email không được để trống.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }

        public TaiKhoan Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new Exception("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.");

            string hashedPassword = HashPassword(password);

            var account = _repo.GetAll()
                               .Find(x => x.TenDangNhap == username && x.MatKhau == hashedPassword);

            if (account == null)
                throw new Exception("Tên đăng nhập hoặc mật khẩu không đúng.");

            if (account.TrangThai == "Đã khóa" || account.TrangThai == "Tạm ngừng")
                throw new Exception("Tài khoản đã bị khóa hoặc tạm ngừng.");

            return account;
        }

        public bool ChangePassword(int id, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                throw new Exception("Mật khẩu không được để trống.");

            var acc = _repo.GetById(id);
            if (acc == null)
                throw new Exception("Tài khoản không tồn tại.");

            acc.MatKhau = HashPassword(newPassword);
            _repo.Update(acc);
            return true;
        }
    }
}
