using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class TaiKhoanRepository
    {
        private readonly string _connectionString = GlobalVariables.ConnectionString;

        private void ExecuteNonQuery(string query, Action<SqlCommand> paramMap)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand(query, conn, trans))
                        {
                            paramMap(cmd);
                            cmd.ExecuteNonQuery();
                            trans.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception($"Lỗi SQL: {ex.Message}");
                    }
                }
            }
        }

        private List<TaiKhoan> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<TaiKhoan>();
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    paramMap?.Invoke(cmd);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new TaiKhoan
                            {
                                MaTaiKhoan = Convert.ToInt32(reader["MaTaiKhoan"]),
                                MaNV = reader["MaNV"].ToString(),
                                TenDangNhap = reader["TenDangNhap"].ToString(),
                                MatKhau = reader["MatKhau"].ToString(),
                                Email = reader["Email"].ToString(),
                                TrangThai = reader["TrangThai"].ToString()
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<TaiKhoan> GetAll() => ExecuteQuery("SELECT * FROM TaiKhoan");

        public TaiKhoan GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM TaiKhoan WHERE MaTaiKhoan = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(TaiKhoan tk)
        {
            ExecuteNonQuery(@"INSERT INTO TaiKhoan (MaNV, TenDangNhap, MatKhau, Email, TrangThai) 
                              VALUES (@MaNV, @TenDangNhap, @MatKhau, @Email, @TrangThai)", cmd =>
            {
                cmd.Parameters.AddWithValue("@MaNV", tk.MaNV);
                cmd.Parameters.AddWithValue("@TenDangNhap", tk.TenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", tk.MatKhau);
                cmd.Parameters.AddWithValue("@Email", tk.Email);
                cmd.Parameters.AddWithValue("@TrangThai", (object)tk.TrangThai ?? DBNull.Value);
            });
        }

        public void Update(TaiKhoan tk)
        {
            ExecuteNonQuery(@"UPDATE TaiKhoan SET 
                                MaNV = @MaNV, 
                                TenDangNhap = @TenDangNhap, 
                                MatKhau = @MatKhau, 
                                Email = @Email, 
                                TrangThai = @TrangThai 
                              WHERE MaTaiKhoan = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", tk.MaTaiKhoan);
                cmd.Parameters.AddWithValue("@MaNV", tk.MaNV);
                cmd.Parameters.AddWithValue("@TenDangNhap", tk.TenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", tk.MatKhau);
                cmd.Parameters.AddWithValue("@Email", tk.Email);
                cmd.Parameters.AddWithValue("@TrangThai", (object)tk.TrangThai ?? DBNull.Value);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM TaiKhoan WHERE MaTaiKhoan = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
