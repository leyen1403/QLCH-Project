using QLCH.BLL;
using QLCH.BLL.Interfaces;
using QLCH.BLL.Services;
using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCH.GUI
{
    public partial class LoginForm : Form
    {
        private readonly INhaCungCapService _nhaCungCapService;
        public LoginForm()
        {
            InitializeComponent();
            _nhaCungCapService = new NhaCungCapService();
            if (!testConnection())
            {               
                SettingForm frm = new SettingForm();
                frm.ShowDialog();
            }          
        }
        
        private bool testConnection()
        {
            try
            {
                var list = _nhaCungCapService.GetAllNhaCungCap();
                return true;
            }
            catch (Exception ex)
            {             
                return false;
            }
        }
    }
}
