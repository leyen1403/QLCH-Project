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
        private readonly IManHinhService _manHinhService;
        public LoginForm()
        {
            InitializeComponent();
            _manHinhService = new ManHinhService();
            if (!testConnection())
            {               
                SettingForm frm = new SettingForm();
                frm.ShowDialog();
                _manHinhService = new ManHinhService();
            } 
        }
        
        private bool testConnection()
        {
            try
            {
                var item = _manHinhService.GetAll();
                return true;
            }
            catch (Exception ex)
            {             
                return false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
        }
    }
}
