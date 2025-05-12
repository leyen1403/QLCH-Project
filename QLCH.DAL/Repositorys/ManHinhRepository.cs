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
    public class ManHinhRepository
    {
        private readonly string _connectionString;
        public ManHinhRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyAppConnectionString"].ConnectionString;
        }

        public List<ManHinh> GetAll()
        {
            var list = new List<ManHinh>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM ManHinh", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ManHinh manHinh = new ManHinh();
                    manHinh.MaMH = reader["MaMH"].ToString();
                    manHinh.TenMH = reader["TenMH"].ToString();
                    manHinh.MoTa = reader["MoTa"].ToString();
                    manHinh.HinhAnh = reader["HinhAnh"].ToString();
                    manHinh.ThoiGianTao = DateTime.Parse(reader["ThoiGianTao"].ToString());
                    manHinh.ThoiGianCapNhat = DateTime.Parse(reader["ThoiGianCapNhat"].ToString());
                    manHinh.TrangThai = (bool)reader["TrangThai"];
                    list.Add(manHinh);
                }
            }
            return list;
        }

        public ManHinh GetById(string maMH)
        {
            ManHinh manHinh = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM ManHinh WHERE MaMH = @MaMH", connection);
                command.Parameters.AddWithValue("@MaMH", maMH);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    manHinh = new ManHinh
                    {
                        MaMH = reader["MaMH"].ToString(),
                        TenMH = reader["TenMH"].ToString(),
                        MoTa = reader["MoTa"].ToString(),
                        HinhAnh = reader["HinhAnh"].ToString(),
                        ThoiGianTao = DateTime.Parse(reader["ThoiGianTao"].ToString()),
                        ThoiGianCapNhat = DateTime.Parse(reader["ThoiGianCapNhat"].ToString()),
                        TrangThai = reader["TrangThai"].ToString() == "1" // Fix: Convert int to bool
                    };
                }
            }
            return manHinh;
        }

        public bool Add(ManHinh manHinh)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                /*
                    INSERT INTO [dbo].[ManHinh]
                               ([MaMH]
                               ,[TenMH]
                               ,[MoTa]
                               ,[HinhAnh]
                               ,[ThoiGianTao]
                               ,[ThoiGianCapNhat]
                               ,[TrangThai])
                         VALUES
                               (<MaMH, nvarchar(20),>
                               ,<TenMH, nvarchar(255),>
                               ,<MoTa, nvarchar(max),>
                               ,<HinhAnh, nvarchar(255),>
                               ,<ThoiGianTao, datetime,>
                               ,<ThoiGianCapNhat, datetime,>
                               ,<TrangThai, bit,>) 
                */
                string query = "INSERT INTO ManHinh (MaMH, TenMH, MoTa, HinhAnh, ThoiGianTao, ThoiGianCapNhat, TrangThai) VALUES (@MaMH, @TenMH, @MoTa, @HinhAnh, @ThoiGianTao, @ThoiGianCapNhat, @TrangThai)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaMH", manHinh.MaMH);
                command.Parameters.AddWithValue("@TenMH", manHinh.TenMH);
                command.Parameters.AddWithValue("@MoTa", manHinh.MoTa);
                command.Parameters.AddWithValue("@HinhAnh", manHinh.HinhAnh);
                command.Parameters.AddWithValue("@ThoiGianTao", manHinh.ThoiGianTao);
                command.Parameters.AddWithValue("@ThoiGianCapNhat", manHinh.ThoiGianCapNhat);
                command.Parameters.AddWithValue("@TrangThai", manHinh.TrangThai);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
        }
    }
}
