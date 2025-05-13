using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using QLCH.DAL.Models;

namespace QLCH.DAL.Repositorys
{
    public class TaiKhoanRepository
    {
        private readonly string _connectionString;

        public TaiKhoanRepository()
        {
            if (!GlobalVariables.IsTestMode)
            {
                _connectionString = ConfigurationManager.ConnectionStrings["MyAppConnectionString"].ConnectionString;
            }
            else
            {
                _connectionString = GlobalVariables.ConnectionString;
            }
        }

        // Thêm mới tài khoản
        public void Add(TaiKhoan taiKhoan)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    INSERT INTO TaiKhoan (
                        MaTK, MaNV, TenDangNhap, MatKhau, ThoiGianTao, ThoiGianCapNhat,
                        TrangThai
                    ) VALUES (
                        @MaTK, @MaNV, @TenDangNhap, @MatKhau, @ThoiGianTao, @ThoiGianCapNhat,
                        @TrangThai
                    );";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    AddParameters(command, taiKhoan);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Lấy thông tin tài khoản theo mã
        public TaiKhoan GetByID(string maTK)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM TaiKhoan WHERE MaTK = @MaTK";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@MaTK", SqlDbType.NVarChar).Value = maTK;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapTaiKhoan(reader);
                        }
                    }
                }
            }
            return null;
        }

        // Lấy toàn bộ danh sách tài khoản
        public List<TaiKhoan> GetAll()
        {
            var list = new List<TaiKhoan>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM TaiKhoan";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapTaiKhoan(reader));
                        }
                    }
                }
            }
            return list;
        }

        // Cập nhật thông tin tài khoản
        public void Update(TaiKhoan taiKhoan)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    UPDATE TaiKhoan SET
                        MaNV = @MaNV,
                        TenDangNhap = @TenDangNhap,
                        MatKhau = @MatKhau,
                        ThoiGianCapNhat = @ThoiGianCapNhat,
                        TrangThai = @TrangThai
                    WHERE MaTK = @MaTK;
                ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    AddParameters(command, taiKhoan);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Xóa tài khoản
        public void Delete(string maTK)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM TaiKhoan WHERE MaTK = @MaTK";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@MaTK", SqlDbType.NVarChar).Value = maTK;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Thêm tham số vào SQL Command
        private void AddParameters(SqlCommand command, TaiKhoan taiKhoan)
        {
            command.Parameters.Add("@MaTK", SqlDbType.NVarChar).Value = taiKhoan.MaTK ?? (object)DBNull.Value;
            command.Parameters.Add("@MaNV", SqlDbType.NVarChar).Value = taiKhoan.MaNV ?? (object)DBNull.Value;
            command.Parameters.Add("@TenDangNhap", SqlDbType.NVarChar).Value = taiKhoan.TenDangNhap ?? (object)DBNull.Value;
            command.Parameters.Add("@MatKhau", SqlDbType.NVarChar).Value = taiKhoan.MatKhau ?? (object)DBNull.Value;
            command.Parameters.Add("@ThoiGianTao", SqlDbType.DateTime).Value = taiKhoan.ThoiGianTao;
            command.Parameters.Add("@ThoiGianCapNhat", SqlDbType.DateTime).Value = taiKhoan.ThoiGianCapNhat;
            command.Parameters.Add("@TrangThai", SqlDbType.Bit).Value = taiKhoan.TrangThai;
        }

        // Map dữ liệu từ DataReader sang đối tượng
        private TaiKhoan MapTaiKhoan(SqlDataReader reader)
        {
            return new TaiKhoan
            {
                MaTK = reader["MaTK"].ToString(),
                MaNV = reader["MaNV"].ToString(),
                TenDangNhap = reader["TenDangNhap"].ToString(),
                MatKhau = reader["MatKhau"].ToString(),
                ThoiGianTao = Convert.ToDateTime(reader["ThoiGianTao"]),
                ThoiGianCapNhat = Convert.ToDateTime(reader["ThoiGianCapNhat"]),
                TrangThai = Convert.ToBoolean(reader["TrangThai"])
            };
        }

        public TaiKhoan GetByUsername(string username)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@TenDangNhap", SqlDbType.NVarChar).Value = username;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TaiKhoan
                            {
                                MaTK = reader["MaTK"].ToString(),
                                MaNV = reader["MaNV"].ToString(),
                                TenDangNhap = reader["TenDangNhap"].ToString(),
                                MatKhau = reader["MatKhau"].ToString(),
                                ThoiGianTao = Convert.ToDateTime(reader["ThoiGianTao"]),
                                ThoiGianCapNhat = Convert.ToDateTime(reader["ThoiGianCapNhat"]),
                                TrangThai = Convert.ToBoolean(reader["TrangThai"])
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
