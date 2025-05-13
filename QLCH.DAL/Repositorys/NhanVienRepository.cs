using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLCH.DAL.Models;

namespace QLCH.DAL.Repositorys
{
    public class NhanVienRepository
    {
        private readonly string _connectionString;
        public NhanVienRepository()
        {
            // Nếu là chế độ test thì dùng connection string trong app.config
            // Nếu không thì dùng connection string trong GlobalVariables
            if (GlobalVariables.IsTestMode)
            {
                _connectionString = ConfigurationManager.ConnectionStrings["TestConnectionString"].ConnectionString;
            }
            else
            {
                _connectionString = GlobalVariables.ConnectionString;
            }
        }
        public void Add(NhanVien nhanVien)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    INSERT INTO NhanVien (
                        MaNV, TenNV, DiaChi, SDT, Email, ThoiGianTao, ThoiGianCapNhat,
                        TrangThai, ChucVu, MucLuong
                    ) VALUES (
                        @MaNV, @TenNV, @DiaChi, @SDT, @Email, @ThoiGianTao, @ThoiGianCapNhat,
                        @TrangThai, @ChucVu, @MucLuong
                    );";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    AddParameters(command, nhanVien);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public NhanVien GetByID(string maNV)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM NhanVien WHERE MaNV = @MaNV";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@MaNV", SqlDbType.NVarChar).Value = maNV;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapNhanVien(reader);
                        }
                    }
                }
            }
            return null;
        }

        public List<NhanVien> GetAll()
        {
            var list = new List<NhanVien>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM NhanVien";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapNhanVien(reader));
                        }
                    }
                }
            }
            return list;
        }
        public void Update(NhanVien nhanVien)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    UPDATE NhanVien SET
                        TenNV = @TenNV,
                        DiaChi = @DiaChi,
                        SDT = @SDT,
                        Email = @Email,
                        ThoiGianCapNhat = @ThoiGianCapNhat,
                        TrangThai = @TrangThai,
                        ChucVu = @ChucVu,
                        MucLuong = @MucLuong
                    WHERE MaNV = @MaNV;
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    AddParameters(command, nhanVien);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(string maNV)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@MaNV", SqlDbType.NVarChar).Value = maNV;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void AddParameters(SqlCommand command, NhanVien nhanVien)
        {
            command.Parameters.Add("@MaNV", SqlDbType.NVarChar).Value = nhanVien.MaNV ?? (object)DBNull.Value;
            command.Parameters.Add("@TenNV", SqlDbType.NVarChar).Value = nhanVien.TenNV ?? (object)DBNull.Value;
            command.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = nhanVien.DiaChi ?? (object)DBNull.Value;
            command.Parameters.Add("@SDT", SqlDbType.NVarChar).Value = nhanVien.SDT ?? (object)DBNull.Value;
            command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = nhanVien.Email ?? (object)DBNull.Value;
            command.Parameters.Add("@ThoiGianTao", SqlDbType.DateTime).Value = nhanVien.ThoiGianTao;
            command.Parameters.Add("@ThoiGianCapNhat", SqlDbType.DateTime).Value = nhanVien.ThoiGianCapNhat;
            command.Parameters.Add("@TrangThai", SqlDbType.Bit).Value = nhanVien.TrangThai;
            command.Parameters.Add("@ChucVu", SqlDbType.NVarChar).Value = nhanVien.ChucVu ?? (object)DBNull.Value;
            command.Parameters.Add("@MucLuong", SqlDbType.Decimal).Value = nhanVien.MucLuong;
        }

        // Map dữ liệu từ DataReader sang đối tượng
        private NhanVien MapNhanVien(SqlDataReader reader)
        {
            return new NhanVien
            {
                MaNV = reader["MaNV"].ToString(),
                TenNV = reader["TenNV"].ToString(),
                DiaChi = reader["DiaChi"].ToString(),
                SDT = reader["SDT"].ToString(),
                Email = reader["Email"].ToString(),
                ThoiGianTao = Convert.ToDateTime(reader["ThoiGianTao"]),
                ThoiGianCapNhat = Convert.ToDateTime(reader["ThoiGianCapNhat"]),
                TrangThai = Convert.ToBoolean(reader["TrangThai"]),
                ChucVu = reader["ChucVu"].ToString(),
                MucLuong = Convert.ToDecimal(reader["MucLuong"])
            };
        }
    }
}
