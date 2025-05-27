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
    public partial class ChucVuManager: Form
    {
        private readonly ApiService _api = new ApiService();
        private Timer refreshTimer;
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

                    refreshTimer = new Timer();
                    refreshTimer.Interval = 5000; // 5 giây
                    refreshTimer.Tick += RefreshTimer_Tick;
                    refreshTimer.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}");
                }
            }
        }

        private async void RefreshTimer_Tick(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            using(ChucVuDetailForm form = new ChucVuDetailForm())
            {
                var result = form.ShowDialog();
                if(result == DialogResult.OK)
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
    }
}
