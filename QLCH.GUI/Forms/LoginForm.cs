using QLCH.BLL.Interfaces;
using QLCH.BLL.Services;
using QLCH.DAL.Models;
using QLCH.GUI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCH.GUI
{
    public partial class LoginForm : Form
    {
        private readonly INhanVienService _nhanVienService;
        private readonly ITaiKhoanService _taiKhoanService;
        public LoginForm()
        {
            InitializeComponent();
            _nhanVienService = new NhanVienService();
            _taiKhoanService = new TaiKhoanService();
        }
        
        private bool testConnection()
        {
            try
            {
                _nhanVienService.GetAllNhanViens();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text;
                string password = txtPassword.Text;
                _taiKhoanService.Login(username, password);
                MainForm mainForm = new MainForm();               
                mainForm.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (!testConnection())
            {
                SettingForm frm = new SettingForm();
                frm.ShowDialog();

                // Nếu cấu hình thành công, thử kết nối lại
                if (frm.IsConfigured)
                {
                    if (testConnection())
                    {
                        MessageBox.Show("Kết nối thành công sau khi cấu hình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Kết nối không thành công. Vui lòng kiểm tra lại cấu hình.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }
                }
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            SettingForm frm = new SettingForm();
            frm.ShowDialog();

            // 🔹 Nếu cấu hình thành công, thử kết nối lại
            if (frm.IsConfigured)
            {
                if (testConnection())
                {
                    MessageBox.Show("Kết nối thành công sau khi cấu hình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Kết nối không thành công. Vui lòng kiểm tra lại cấu hình.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
