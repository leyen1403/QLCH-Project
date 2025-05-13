using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL
{
    public static class GlobalVariables
    {
        // Connection String (Dùng chung cho DAL)
        public static string ConnectionString { get; set; } = "Data Source=DESKTOP-UOPCAO3\\HJSV;Initial Catalog=QLCH;Integrated Security=True;TrustServerCertificate=True";

        // Thông tin người dùng hiện tại
        public static string CurrentUser { get; set; } = "Guest";
        public static string CurrentRole { get; set; } = "Viewer";
        public static bool IsLoggedIn { get; set; } = false;

        // Phiên làm việc
        public static string SessionID { get; set; } = string.Empty;

        // Cấu hình ứng dụng
        public static bool IsDebugMode { get; set; } = false;
    }
}
