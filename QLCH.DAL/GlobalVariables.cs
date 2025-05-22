using QLCH.DAL.Models;
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

        public static List<PhongBan> g_listPhongBan = new List<PhongBan>();
        public static List<ChucVu> g_listChucVu = new List<ChucVu>();
        public static List<CuaHang> g_listCuaHang = new List<CuaHang>();
    }
}
