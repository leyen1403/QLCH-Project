using QLCH.BLL.Common.Enums;
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
    public partial class NhanVienDetailForm : Form
    {
        private FormMode _mode;
        private string _maNV;
        public NhanVienDetailForm(FormMode mode, string maNV = null)
        {
            InitializeComponent();
            _mode = mode;
            _maNV = maNV;
        }

        private void NhanVienDetailForm_Load(object sender, EventArgs e)
        {
            if(_mode == FormMode.Insert)
            {
                InitForInsert();
            }
            else if (_mode == FormMode.Update)
            {
                InitForUpdate();
            }
        }

        private void InitForUpdate()
        {
            throw new NotImplementedException();
        }

        private void InitForInsert()
        {
            throw new NotImplementedException();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

        }

        private void btnDong_Click(object sender, EventArgs e)
        {

        }
    }
}
