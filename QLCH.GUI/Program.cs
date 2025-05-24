using QLCH.BLL.Common.Enums;
using QLCH.DAL;
using QLCH.GUI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCH.GUI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SystemInitializer.EnsureAdminAccount();
            //Application.Run(new LoginForm());
            Application.Run(new NhanVienManagerForm());
            //Application.Run(new NhanVienDetailForm(FormMode.Insert));
            //Application.Run(new NhanVienDetailForm(FormMode.Update, "NV0002"));
        }
    }
}
