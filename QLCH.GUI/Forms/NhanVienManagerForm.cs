using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLCH.BLL;
using QLCH.BLL.Interfaces;
using QLCH.BLL.Services;

namespace QLCH.GUI.Forms
{
    public partial class NhanVienManagerForm: Form
    {
        private readonly NhanVienService _nhanVienService;

        public NhanVienManagerForm()
        {
            InitializeComponent();
            _nhanVienService = new NhanVienService();
        }

        private void LoadData()
        {
            try
            {
                if (_nhanVienService == null)
                {
                    MessageBox.Show("Service chưa được khởi tạo.");
                    return;
                }
                var nhanVienList = _nhanVienService.GetAllNhanViens();
                if(nhanVienList == null || nhanVienList.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu nhân viên.");
                    return;
                }
                ListNhanVienGirdView.DataSource = nhanVienList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void NhanVienManagerForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
