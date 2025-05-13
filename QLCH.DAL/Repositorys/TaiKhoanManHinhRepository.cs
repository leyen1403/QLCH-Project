using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QLCH.DAL.Repositorys
{
    public class TaiKhoanManHinhRepository
    {
        private readonly string _connectionString;

        public TaiKhoanManHinhRepository()
        {
            if (!GlobalVariables.IsTestMode)
                _connectionString = ConfigurationManager.ConnectionStrings["MyAppConnectionString"].ConnectionString;
            else
                _connectionString = GlobalVariables.ConnectionString;
        }

        // 🔹 Thêm mới tài khoản - màn hình
        public void Add(TaiKhoanManHinh taiKhoanManHinh)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    INSERT INTO TaiKhoanManHinh (
                        MaTK, MaMH, ThoiGianTao, ThoiGianCapNhat, TrangThai
                    ) VALUES (
                        @MaTK, @MaMH, @ThoiGianTao, @ThoiGianCapNhat, @TrangThai
                    );";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    AddParameters(command, taiKhoanManHinh);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // 🔹 Lấy thông tin tài khoản - màn hình theo mã
        public TaiKhoanManHinh GetByID(string maTK, string maMH)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM TaiKhoanManHinh WHERE MaTK = @MaTK AND MaMH = @MaMH";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@MaTK", SqlDbType.NVarChar).Value = maTK;
                    command.Parameters.Add("@MaMH", SqlDbType.NVarChar).Value = maMH;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapTaiKhoanManHinh(reader);
                        }
                    }
                }
            }
            return null;
        }

        // 🔹 Lấy toàn bộ danh sách tài khoản - màn hình
        public List<TaiKhoanManHinh> GetAll()
        {
            var list = new List<TaiKhoanManHinh>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM TaiKhoanManHinh";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapTaiKhoanManHinh(reader));
                        }
                    }
                }
            }
            return list;
        }

        // 🔹 Cập nhật thông tin tài khoản - màn hình
        public void Update(TaiKhoanManHinh taiKhoanManHinh)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    UPDATE TaiKhoanManHinh SET
                        ThoiGianCapNhat = @ThoiGianCapNhat,
                        TrangThai = @TrangThai
                    WHERE MaTK = @MaTK AND MaMH = @MaMH;
                ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    AddParameters(command, taiKhoanManHinh);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // 🔹 Xóa tài khoản - màn hình
        public void Delete(string maTK, string maMH)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM TaiKhoanManHinh WHERE MaTK = @MaTK AND MaMH = @MaMH";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@MaTK", SqlDbType.NVarChar).Value = maTK;
                    command.Parameters.Add("@MaMH", SqlDbType.NVarChar).Value = maMH;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // 🔹 Thêm tham số vào SQL Command
        private void AddParameters(SqlCommand command, TaiKhoanManHinh taiKhoanManHinh)
        {
            command.Parameters.Add("@MaTK", SqlDbType.NVarChar).Value = taiKhoanManHinh.MaTK ?? (object)DBNull.Value;
            command.Parameters.Add("@MaMH", SqlDbType.NVarChar).Value = taiKhoanManHinh.MaMH ?? (object)DBNull.Value;
            command.Parameters.Add("@ThoiGianTao", SqlDbType.DateTime).Value = taiKhoanManHinh.ThoiGianTao;
            command.Parameters.Add("@ThoiGianCapNhat", SqlDbType.DateTime).Value = taiKhoanManHinh.ThoiGianCapNhat;
            command.Parameters.Add("@TrangThai", SqlDbType.Bit).Value = taiKhoanManHinh.TrangThai;
        }

        // 🔹 Map dữ liệu từ DataReader sang đối tượng
        private TaiKhoanManHinh MapTaiKhoanManHinh(SqlDataReader reader)
        {
            return new TaiKhoanManHinh
            {
                MaTK = reader["MaTK"].ToString(),
                MaMH = reader["MaMH"].ToString(),
                ThoiGianTao = Convert.ToDateTime(reader["ThoiGianTao"]),
                ThoiGianCapNhat = Convert.ToDateTime(reader["ThoiGianCapNhat"]),
                TrangThai = Convert.ToBoolean(reader["TrangThai"])
            };
        }
    }
}
