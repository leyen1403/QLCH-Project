using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL
{
    public static class GlobalVariables
    {
        // Connection String (Dùng chung cho DAL)
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["MyAppConnectionString"].ConnectionString;

        // Chế độ test
        public static bool IsTestMode { get; set; } = false;
    }
}
