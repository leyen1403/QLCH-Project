namespace QLCH.GUI.Forms
{
    partial class NhanVienManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.customTitle1 = new QLCH.GUI.Custom.CustomTitle();
            this.userDataGridView1 = new QLCH.GUI.UserControls.UserDataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.customTitle1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1419, 105);
            this.panel1.TabIndex = 1;
            // 
            // customTitle1
            // 
            this.customTitle1.AutoSize = true;
            this.customTitle1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.customTitle1.ForeColor = System.Drawing.Color.DarkBlue;
            this.customTitle1.Location = new System.Drawing.Point(605, 21);
            this.customTitle1.Name = "customTitle1";
            this.customTitle1.Size = new System.Drawing.Size(448, 37);
            this.customTitle1.TabIndex = 0;
            this.customTitle1.Text = "QUẢN LÝ THÔNG TIN NHÂN VIÊN";
            this.customTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // userDataGridView1
            // 
            this.userDataGridView1.AllowUserToAddRows = false;
            this.userDataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.userDataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.userDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.userDataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userDataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.userDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.userDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.userDataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.userDataGridView1.EnableHeadersVisualStyles = false;
            this.userDataGridView1.Location = new System.Drawing.Point(0, 310);
            this.userDataGridView1.Name = "userDataGridView1";
            this.userDataGridView1.RowHeadersVisible = false;
            this.userDataGridView1.RowHeadersWidth = 51;
            this.userDataGridView1.RowTemplate.Height = 40;
            this.userDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.userDataGridView1.Size = new System.Drawing.Size(2218, 1304);
            this.userDataGridView1.TabIndex = 0;
            // 
            // NhanVienManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1419, 1033);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.userDataGridView1);
            this.Name = "NhanVienManagerForm";
            this.Text = "NhanVienManagerForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userDataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.UserDataGridView userDataGridView1;
        private System.Windows.Forms.Panel panel1;
        private Custom.CustomTitle customTitle1;
    }
}