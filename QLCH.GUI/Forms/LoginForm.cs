using QLCH.BLL;
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
        private readonly IManHinhService _manHinhService;
        private readonly ITaiKhoan _taiKhoan;
        public LoginForm()
        {
            InitializeComponent();
            _taiKhoan = new TaiKhoanService();
            _manHinhService = new ManHinhService();            
        }
        
        private bool testConnection()
        {
            try
            {
                var item = _manHinhService.GetAll();
                return true;
            }
            catch (Exception ex)
            {             
                return false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text;
                string password = txtPassword.Text;
                var taiKhoan = _taiKhoan.Login(username, password);
                MainForm mainForm = new MainForm();
                mainForm.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi: "+ ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (!testConnection())
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
