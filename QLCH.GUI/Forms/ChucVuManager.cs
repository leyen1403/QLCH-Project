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
                    var list = await _api.GetListAsync<ChucVuDto>("/api/ChucVu");

                    g_list1.Columns.Clear();
                    g_list1.AutoGenerateColumns = true;
                    g_list1.DataSource = list;

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}");
                }
            }
        }

    }
}
