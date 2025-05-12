using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL
{
    public class NhaCungCapRepository
    {
        private readonly string _connectionString;
        public NhaCungCapRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyAppConnectionString"].ConnectionString;
        }
        public List<NhaCungCap> GetAll()
        {
            var nhaCungCaps = new List<NhaCungCap>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM NhaCungCap", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    nhaCungCaps.Add(new NhaCungCap
                    {
                        MaNCC = reader["MaNCC"].ToString(),
                        TenNCC = reader["TenNCC"].ToString(),
                        DiaChi = reader["DiaChi"].ToString(),
                        SDT = reader["SDT"].ToString(),
                        Email = reader["Email"].ToString(),
                        Website = reader["Website"].ToString(),
                        ThoiGianTao = DateTime.Parse(reader["ThoiGianTao"].ToString()),
                        ThoiGianCapNhat = DateTime.Parse(reader["ThoiGianCapNhat"].ToString()),
                        TrangThai = (TrangThaiNhaCungCap)Enum.Parse(typeof(TrangThaiNhaCungCap), reader["TrangThai"].ToString())
                    });
                }
            }
            return nhaCungCaps;
        }
    }
}
