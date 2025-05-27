namespace QLCH.GUI.Forms
{
    partial class ChucVuDetailForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMaCV = new System.Windows.Forms.TextBox();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.customLabel4 = new QLCH.GUI.Custom.CustomLabel();
            this.txtHSL = new System.Windows.Forms.TextBox();
            this.customLabel3 = new QLCH.GUI.Custom.CustomLabel();
            this.txtTenCV = new System.Windows.Forms.TextBox();
            this.customLabel2 = new QLCH.GUI.Custom.CustomLabel();
            this.customLabel1 = new QLCH.GUI.Custom.CustomLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.customTitle1 = new QLCH.GUI.Custom.CustomTitle();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.txtMaCV);
            this.panel1.Controls.Add(this.btnHuy);
            this.panel1.Controls.Add(this.btnLuu);
            this.panel1.Controls.Add(this.txtMoTa);
            this.panel1.Controls.Add(this.customLabel4);
            this.panel1.Controls.Add(this.txtHSL);
            this.panel1.Controls.Add(this.customLabel3);
            this.panel1.Controls.Add(this.txtTenCV);
            this.panel1.Controls.Add(this.customLabel2);
            this.panel1.Controls.Add(this.customLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(430, 402);
            this.panel1.TabIndex = 1;
            // 
            // txtMaCV
            // 
            this.txtMaCV.Enabled = false;
            this.txtMaCV.Location = new System.Drawing.Point(133, 28);
            this.txtMaCV.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMaCV.Name = "txtMaCV";
            this.txtMaCV.Size = new System.Drawing.Size(178, 20);
            this.txtMaCV.TabIndex = 12;
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(182, 301);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(56, 19);
            this.btnHuy.TabIndex = 10;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(254, 301);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(56, 19);
            this.btnLuu.TabIndex = 9;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // txtMoTa
            // 
            this.txtMoTa.Location = new System.Drawing.Point(133, 148);
            this.txtMoTa.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(178, 125);
            this.txtMoTa.TabIndex = 8;
            // 
            // customLabel4
            // 
            this.customLabel4.AutoSize = true;
            this.customLabel4.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.customLabel4.ForeColor = System.Drawing.Color.DarkBlue;
            this.customLabel4.Location = new System.Drawing.Point(19, 145);
            this.customLabel4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.customLabel4.Name = "customLabel4";
            this.customLabel4.Size = new System.Drawing.Size(50, 21);
            this.customLabel4.TabIndex = 7;
            this.customLabel4.Text = "Mô tả";
            // 
            // txtHSL
            // 
            this.txtHSL.Location = new System.Drawing.Point(133, 105);
            this.txtHSL.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtHSL.Name = "txtHSL";
            this.txtHSL.Size = new System.Drawing.Size(178, 20);
            this.txtHSL.TabIndex = 6;
            // 
            // customLabel3
            // 
            this.customLabel3.AutoSize = true;
            this.customLabel3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.customLabel3.ForeColor = System.Drawing.Color.DarkBlue;
            this.customLabel3.Location = new System.Drawing.Point(19, 102);
            this.customLabel3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.customLabel3.Name = "customLabel3";
            this.customLabel3.Size = new System.Drawing.Size(94, 21);
            this.customLabel3.TabIndex = 5;
            this.customLabel3.Text = "Hệ số lương";
            // 
            // txtTenCV
            // 
            this.txtTenCV.Location = new System.Drawing.Point(133, 63);
            this.txtTenCV.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTenCV.Name = "txtTenCV";
            this.txtTenCV.Size = new System.Drawing.Size(178, 20);
            this.txtTenCV.TabIndex = 4;
            // 
            // customLabel2
            // 
            this.customLabel2.AutoSize = true;
            this.customLabel2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.customLabel2.ForeColor = System.Drawing.Color.DarkBlue;
            this.customLabel2.Location = new System.Drawing.Point(19, 60);
            this.customLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.customLabel2.Name = "customLabel2";
            this.customLabel2.Size = new System.Drawing.Size(90, 21);
            this.customLabel2.TabIndex = 3;
            this.customLabel2.Text = "Tên chức vụ";
            // 
            // customLabel1
            // 
            this.customLabel1.AutoSize = true;
            this.customLabel1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.customLabel1.ForeColor = System.Drawing.Color.DarkBlue;
            this.customLabel1.Location = new System.Drawing.Point(19, 23);
            this.customLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(89, 21);
            this.customLabel1.TabIndex = 0;
            this.customLabel1.Text = "Mã chức vụ";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.customTitle1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(430, 49);
            this.panel2.TabIndex = 2;
            // 
            // customTitle1
            // 
            this.customTitle1.AutoSize = true;
            this.customTitle1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.customTitle1.ForeColor = System.Drawing.Color.DarkBlue;
            this.customTitle1.Location = new System.Drawing.Point(127, 7);
            this.customTitle1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.customTitle1.Name = "customTitle1";
            this.customTitle1.Size = new System.Drawing.Size(111, 37);
            this.customTitle1.TabIndex = 0;
            this.customTitle1.Text = "Tiêu đề";
            this.customTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChucVuDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 456);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ChucVuDetailForm";
            this.Text = "ChucVuDetailForm";
            this.Load += new System.EventHandler(this.ChucVuDetailForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private Custom.CustomLabel customLabel1;
        private System.Windows.Forms.TextBox txtMoTa;
        private Custom.CustomLabel customLabel4;
        private System.Windows.Forms.TextBox txtHSL;
        private Custom.CustomLabel customLabel3;
        private System.Windows.Forms.TextBox txtTenCV;
        private Custom.CustomLabel customLabel2;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Panel panel2;
        private Custom.CustomTitle customTitle1;
        private System.Windows.Forms.TextBox txtMaCV;
    }
}