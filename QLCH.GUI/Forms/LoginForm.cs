using QLCH.BLL.Common.Enums;
using QLCH.BLL.Interfaces;
using QLCH.BLL.Services;
using QLCH.DAL;
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
        private readonly IChucVuService _chucVuSer;
        private readonly IPhongBanService _phongBanSer;
        private readonly ICuaHangService _cuaHangSer;
        public LoginForm()
        {
            InitializeComponent();
            _nhanVienService = new NhanVienService();
            _taiKhoanService = new TaiKhoanService();
            _chucVuSer = new ChucVuService();
            _phongBanSer = new PhongBanService();
            _cuaHangSer = new CuaHangService();
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
                initGlobalVariable();
                Hide();
                var f = new NhanVienDetailForm(FormMode.Insert);
                f.ShowDialog();
                Close();
                //MainForm mainForm = new MainForm();               
                //mainForm.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void initGlobalVariable()
        {
            GlobalVariables.g_listChucVu = _chucVuSer.GetAll();
            GlobalVariables.g_listPhongBan = _phongBanSer.GetAll();
            GlobalVariables.g_listCuaHang = _cuaHangSer.GetAll();
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
                    MessageBox.Show("Kết nối thành công sau khi cấu hình!\nVui lòng khởi động lại hệ thống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
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
