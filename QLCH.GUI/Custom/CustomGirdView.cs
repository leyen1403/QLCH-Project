using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLCH.GUI.Custom
{
    class CustomGirdView : DataGridView
    {
        public CustomGirdView()
        {
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToResizeColumns = false;
            this.AllowUserToResizeRows = false;
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.BackgroundColor = Color.White;
            this.BorderStyle = BorderStyle.None;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RowHeadersVisible = false;
            this.EnableHeadersVisualStyles = false;

            // Header style
            this.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;
            this.ColumnHeadersDefaultCellStyle.ForeColor = Color.DarkBlue;
            this.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Dòng chẵn (default): LightSkyBlue
            this.DefaultCellStyle.BackColor = Color.LightSkyBlue;
            this.DefaultCellStyle.ForeColor = Color.DarkBlue;
            this.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            // Dòng lẻ: White
            this.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            this.AlternatingRowsDefaultCellStyle.ForeColor = Color.DarkBlue;
            this.AlternatingRowsDefaultCellStyle.Font = new Font("Segoe UI", 10);

            // Khi chọn dòng
            this.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            this.DefaultCellStyle.SelectionForeColor = Color.White;
            this.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            this.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
        }

        protected override void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
        {
            base.OnCellFormatting(e);
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                e.Value = e.RowIndex + 1;
                e.FormattingApplied = true;
            }
        }
    }
}
