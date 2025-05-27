using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using QLCH.GUI.DTOs;
using QLCH.GUI.Services;


namespace QLCH.GUI.Forms
{
    public partial class ChucVuManager : Form
    {
        private readonly ApiService _api = new ApiService();
        public ChucVuManager()
        {
            InitializeComponent();
        }

        private async void ChucVuManager_Load(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    await LoadDataAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}");
                }
            }
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            using (ChucVuDetailForm form = new ChucVuDetailForm())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    await LoadDataAsync();
                }
            }
        }

        private async Task LoadDataAsync()
        {
            var list = await _api.GetListAsync<ChucVuDto>("/api/ChucVu");
            dgvListCV.DataSource = list;
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            int id = dgvListCV.CurrentRow.Cells[0].Value.ToString() != null ? Convert.ToInt32(dgvListCV.CurrentRow.Cells[0].Value) : 0;
            if (id == 0)
            {
                MessageBox.Show("Vui lòng chọn chức vụ để xóa.");
                return;
            }
            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa chức vụ này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    var success = await _api.PostAsync($"/api/ChucVu/Delete/{id}");
                    if (success)
                    {
                        await LoadDataAsync();
                    }
                    else
                    {
                        MessageBox.Show("Xóa chức vụ không thành công. Vui lòng thử lại sau.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
                }
            }
        } 
    }
}
