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
            if(GlobalVariables.IsTestMode)
            {
                _connectionString = GlobalVariables.ConnectionString;
            }
            else
            {                
                _connectionString = ConfigurationManager.ConnectionStrings["MyAppConnectionString"].ConnectionString;
            }
        }

        // 🔹 Lấy toàn bộ danh sách
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
                    list.Add(MapManHinh(reader));
                }
            }
            return list;
        }

        // 🔹 Lấy thông tin theo mã
        public ManHinh GetById(string maMH)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM ManHinh WHERE MaMH = @MaMH", connection);
                command.Parameters.AddWithValue("@MaMH", maMH);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapManHinh(reader);
                }
            }
            return null;
        }

        // 🔹 Thêm mới màn hình
        public bool Add(ManHinh manHinh)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO ManHinh (MaMH, TenMH, MoTa, HinhAnh, ThoiGianTao, ThoiGianCapNhat, TrangThai) 
                                 VALUES (@MaMH, @TenMH, @MoTa, @HinhAnh, @ThoiGianTao, @ThoiGianCapNhat, @TrangThai)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    AddParameters(command, manHinh);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }

        // 🔹 Cập nhật thông tin màn hình
        public bool Update(ManHinh manHinh)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE ManHinh SET
                                    TenMH = @TenMH,
                                    MoTa = @MoTa,
                                    HinhAnh = @HinhAnh,
                                    ThoiGianCapNhat = @ThoiGianCapNhat,
                                    TrangThai = @TrangThai
                                WHERE MaMH = @MaMH";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    AddParameters(command, manHinh);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // 🔹 Xóa màn hình
        public bool Delete(string maMH)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM ManHinh WHERE MaMH = @MaMH";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaMH", maMH);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // 🔹 Thêm tham số vào SqlCommand
        private void AddParameters(SqlCommand command, ManHinh manHinh)
        {
            command.Parameters.AddWithValue("@MaMH", manHinh.MaMH);
            command.Parameters.AddWithValue("@TenMH", manHinh.TenMH);
            command.Parameters.AddWithValue("@MoTa", manHinh.MoTa);
            command.Parameters.AddWithValue("@HinhAnh", manHinh.HinhAnh);
            command.Parameters.AddWithValue("@ThoiGianTao", manHinh.ThoiGianTao);
            command.Parameters.AddWithValue("@ThoiGianCapNhat", manHinh.ThoiGianCapNhat);
            command.Parameters.AddWithValue("@TrangThai", manHinh.TrangThai);
        }

        // 🔹 Map dữ liệu từ DataReader sang đối tượng
        private ManHinh MapManHinh(SqlDataReader reader)
        {
            return new ManHinh
            {
                MaMH = reader["MaMH"].ToString(),
                TenMH = reader["TenMH"].ToString(),
                MoTa = reader["MoTa"].ToString(),
                HinhAnh = reader["HinhAnh"].ToString(),
                ThoiGianTao = Convert.ToDateTime(reader["ThoiGianTao"]),
                ThoiGianCapNhat = Convert.ToDateTime(reader["ThoiGianCapNhat"]),
                TrangThai = Convert.ToBoolean(reader["TrangThai"])
            };
        }
    }
}
