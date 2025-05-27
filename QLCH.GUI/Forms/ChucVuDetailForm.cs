using QLCH.GUI.DTOs;
using QLCH.GUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCH.GUI.Forms
{
    public partial class ChucVuDetailForm : Form
    {
        private readonly ApiService _api = new ApiService();

        public ChucVuDetailForm()
        {
            InitializeComponent();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtTenCV.Text))
                {
                    MessageBox.Show("Tên chức vụ không được để trống.");
                    return;
                }
                if (string.IsNullOrEmpty(txtHSL.Text) || Convert.ToDecimal(txtHSL.Text) <= 0)
                {
                    MessageBox.Show("Hệ số lương phải lớn hơn 0.");
                    return;
                }

                ChucVuDto chucVu = new ChucVuDto()
                {
                    MaChucVu = 0,
                    TenChucVu = txtTenCV.Text.Trim(),
                    HeSoLuong = Convert.ToDecimal(txtHSL.Text),
                    MoTa = txtMoTa.Text.Trim()
                };

                var success = await _api.PostAsync("/api/ChucVu/Create", chucVu);

                if (success)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Lưu chức vụ không thành công. Vui lòng thử lại sau.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        private async void btnHuy_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void ChucVuDetailForm_Load(object sender, EventArgs e)
        {

        }
    }
}