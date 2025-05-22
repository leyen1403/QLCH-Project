using QLCH.BLL.Helpers;
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
        private readonly NhanVienRepository _nhanVienRepo = new NhanVienRepository();

        public List<TaiKhoan> GetAll() => _repo.GetAll();

        public TaiKhoan GetById(int id) => _repo.GetById(id);

        public bool Add(TaiKhoan tk)
        {
            try
            {
                ValidationHelper.Validate<TaiKhoan>(tk);
                tk.MatKhau = HashHelper.HashPassword(tk.MatKhau);
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
                ValidationHelper.Validate<TaiKhoan>(tk);
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

        public TaiKhoan Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new Exception("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.");

            string hashedPassword = HashHelper.HashPassword(password);

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

            acc.MatKhau = HashHelper.HashPassword(newPassword);
            _repo.Update(acc);
            return true;
        }

        public List<string> GetMaNVChuaCoTaiKhoan()
        {
            return _nhanVienRepo.GetNhanVienChuaCoTaiKhoan();
        }
    }
}
