using QLCH.BLL;
using QLCH.BLL.Interfaces;
using QLCH.BLL.Services;
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
    public partial class TestForm : Form
    {
        private readonly IManHinhService _manHinhService;
        public TestForm()
        {
            InitializeComponent();
            _manHinhService = new ManHinhService();
            userDataGridView1.DataSource = _manHinhService.GetAll();
        }
    }
}
