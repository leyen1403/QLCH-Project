using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCH.GUI.UserControls
{
    public class UserDataGridView : DataGridView
    {
        public UserDataGridView()
        {
            // Tắt tự động tạo cột
            //AutoGenerateColumns = false;
            AllowUserToAddRows = false;
            AllowUserToResizeRows = false;
            RowHeadersVisible = false;
            BorderStyle = BorderStyle.None;

            // Thiết lập giao diện cho Header
            ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            EnableHeadersVisualStyles = false;
            ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Thiết lập giao diện cho các dòng
            AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            DefaultCellStyle.BackColor = Color.White;
            DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 220, 220);
            DefaultCellStyle.SelectionForeColor = Color.Black;
            DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            DefaultCellStyle.Padding = new Padding(5, 5, 5, 5);
            DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Không hiển thị đường viền của ô
            //CellBorderStyle = DataGridViewCellBorderStyle.None;

            // Thiết lập chiều cao dòng
            RowTemplate.Height = 40;

            // Tự động điều chỉnh chiều rộng cột
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Chọn 1 ô sẽ chọn cả dòng
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
