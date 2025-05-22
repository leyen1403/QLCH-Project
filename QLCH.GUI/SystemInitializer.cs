using QLCH.BLL.Helpers;
using QLCH.BLL.Services;
using QLCH.DAL.Models;
using QLCH.DAL.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCH.GUI
{
    public static class SystemInitializer
    {
        public static void EnsureAdminAccount()
        {
            try
            {
                var taiKhoanSer = new TaiKhoanService();

                if (!taiKhoanSer.GetAll().Any())
                {
                    var admin = new TaiKhoan
                    {
                        MaNV = null, // tài khoản hệ thống
                        TenDangNhap = "admin",
                        MatKhau = "admin123",
                        Email = "admin@localhost",
                        TrangThai = "Hoạt động"
                    };

                    taiKhoanSer.Add(admin);

                    MessageBox.Show(
                        "⚠ Tài khoản admin mặc định đã được tạo:\n\n" +
                        "👤 Tên đăng nhập: admin\n🔐 Mật khẩu: admin123\n\n" +
                        "Vui lòng đổi mật khẩu sau khi đăng nhập.",
                        "Khởi tạo hệ thống",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi khởi tạo tài khoản admin: {ex.Message}",
                    "Khởi tạo hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
        }
    }
}
