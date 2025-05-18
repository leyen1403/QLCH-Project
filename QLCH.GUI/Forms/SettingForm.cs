
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace QLCH.GUI
{
    public partial class SettingForm : Form
    {
        public bool IsConfigured { get; set; } = false;
        public SettingForm()
        {
            InitializeComponent();
            LoadServers();
        }
        //Data Source=DESKTOP-UOPCAO3\HJSV;Initial Catalog=QLCH;Integrated Security=True;Trust Server Certificate=True
        // Load danh sách SQL Server lên ComboBox
        private void LoadServers()
        {
            DataTable servers = SqlDataSourceEnumerator.Instance.GetDataSources();
            foreach (DataRow row in servers.Rows)
            {
                string serverName = row["ServerName"].ToString();
                string instanceName = row["InstanceName"].ToString();
                if (string.IsNullOrEmpty(instanceName))
                    cmbServers.Items.Add(serverName);
                else
                    cmbServers.Items.Add($"{serverName}\\{instanceName}");
            }
            if (cmbServers.Items.Count > 0)
            {
                cmbServers.SelectedIndex = 0;
            }
            cmbServers.SelectedIndexChanged += CmbServers_SelectedIndexChanged;
        }

        // Khi chọn Server, load Database lên
        private void CmbServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDatabaseName.Items.Clear();
            string selectedServer = cmbServers.SelectedItem.ToString();
            string connectionString = $"Data Source={selectedServer};Integrated Security=True";
            LoadDatabase(connectionString);
        }

        // Load danh sách Database của Server
        private void LoadDatabase(string connectionString)
        {
            var databaseName = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT name FROM sys.databases", connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        databaseName.Add(reader["name"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi kết nối đến cơ sở dữ liệu: {ex.Message}");
                }
            }
            cmbDatabaseName.Items.Clear();
            foreach (var db in databaseName)
            {
                cmbDatabaseName.Items.Add(db);
            }
            if (cmbDatabaseName.Items.Count > 0)
            {
                cmbDatabaseName.SelectedIndex = 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbServers.SelectedItem == null || cmbDatabaseName.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn Server và Database.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🔹 Lấy thông tin từ ComboBox
            string server = cmbServers.SelectedItem.ToString();
            string database = cmbDatabaseName.SelectedItem.ToString();
            string connectionString = $"Data Source={server};Initial Catalog={database};Integrated Security=True;TrustServerCertificate=True";

            try
            {
                // Tìm đường dẫn file config
                string exePath = Assembly.GetExecutingAssembly().Location;
                string configPath = $"{exePath}.config";

                // Load file config
                var configMap = new ExeConfigurationFileMap { ExeConfigFilename = configPath };
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

                // Cập nhật giá trị mới
                if (config.ConnectionStrings.ConnectionStrings["MyAppConnectionString"] != null)
                {
                    config.ConnectionStrings.ConnectionStrings["MyAppConnectionString"].ConnectionString = connectionString;
                }
                else
                {
                    config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings
                    {
                        Name = "MyAppConnectionString",
                        ConnectionString = connectionString,
                        ProviderName = "System.Data.SqlClient"
                    });
                }

                // Lưu lại thay đổi
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");

                MessageBox.Show("Cấu hình kết nối đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Đánh dấu là cấu hình thành công
                IsConfigured = true;

                // Thoát khỏi ShowDialog
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể lưu cấu hình: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (cmbServers.SelectedItem == null || cmbDatabaseName.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn Server và Database.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy thông tin từ ComboBox
            string server = cmbServers.SelectedItem.ToString();
            string database = cmbDatabaseName.SelectedItem.ToString();
            string connectionString = $"Data Source={server};Initial Catalog={database};Integrated Security=True;TrustServerCertificate=True";

            // Câu lệnh SQL
            string sql = "SELECT * FROM NhanVien";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    MessageBox.Show("Kết nối thành công và truy vấn dữ liệu hoàn tất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể kết nối hoặc truy vấn lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
