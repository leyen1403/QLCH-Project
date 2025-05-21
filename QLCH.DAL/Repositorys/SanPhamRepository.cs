using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class SanPhamRepository
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

        private List<SanPham> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<SanPham>();
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
                            result.Add(new SanPham
                            {
                                MaSanPham = Convert.ToInt32(reader["MaSanPham"]),
                                TenSanPham = reader["TenSanPham"].ToString(),
                                MaNCC = Convert.ToInt32(reader["MaNCC"]),
                                MoTa = reader["MoTa"]?.ToString(),
                                TrangThai = reader["TrangThai"].ToString()
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<SanPham> GetAll() => ExecuteQuery("SELECT * FROM SanPham");

        public SanPham GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM SanPham WHERE MaSanPham = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(SanPham sp)
        {
            ExecuteNonQuery(@"INSERT INTO SanPham (TenSanPham, MaNCC, MoTa, TrangThai)
                              VALUES (@TenSanPham, @MaNCC, @MoTa, @TrangThai)", cmd =>
            {
                cmd.Parameters.AddWithValue("@TenSanPham", sp.TenSanPham);
                cmd.Parameters.AddWithValue("@MaNCC", sp.MaNCC);
                cmd.Parameters.AddWithValue("@MoTa", (object)sp.MoTa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TrangThai", (object)sp.TrangThai ?? "Hoạt động");
            });
        }

        public void Update(SanPham sp)
        {
            ExecuteNonQuery(@"UPDATE SanPham SET 
                                TenSanPham = @TenSanPham, 
                                MaNCC = @MaNCC, 
                                MoTa = @MoTa, 
                                TrangThai = @TrangThai
                              WHERE MaSanPham = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", sp.MaSanPham);
                cmd.Parameters.AddWithValue("@TenSanPham", sp.TenSanPham);
                cmd.Parameters.AddWithValue("@MaNCC", sp.MaNCC);
                cmd.Parameters.AddWithValue("@MoTa", (object)sp.MoTa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TrangThai", (object)sp.TrangThai ?? "Hoạt động");
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM SanPham WHERE MaSanPham = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
