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

        // Chế độ test
        public static bool IsTestMode { get; set; } = false;
    }
}
