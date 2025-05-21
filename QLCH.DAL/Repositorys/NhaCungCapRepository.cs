using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class NhaCungCapRepository
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

        private List<NhaCungCap> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<NhaCungCap>();
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
                            result.Add(new NhaCungCap
                            {
                                MaNCC = Convert.ToInt32(reader["MaNCC"]),
                                TenNCC = reader["TenNCC"].ToString(),
                                DiaChi = reader["DiaChi"]?.ToString(),
                                SoDienThoai = reader["SoDienThoai"].ToString(),
                                Email = reader["Email"]?.ToString(),
                                MoTa = reader["MoTa"]?.ToString()
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<NhaCungCap> GetAll() => ExecuteQuery("SELECT * FROM NhaCungCap");

        public NhaCungCap GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM NhaCungCap WHERE MaNCC = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(NhaCungCap ncc)
        {
            ExecuteNonQuery(@"INSERT INTO NhaCungCap (TenNCC, DiaChi, SoDienThoai, Email, MoTa)
                              VALUES (@TenNCC, @DiaChi, @SoDienThoai, @Email, @MoTa)", cmd =>
            {
                cmd.Parameters.AddWithValue("@TenNCC", ncc.TenNCC);
                cmd.Parameters.AddWithValue("@DiaChi", (object)ncc.DiaChi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SoDienThoai", ncc.SoDienThoai);
                cmd.Parameters.AddWithValue("@Email", (object)ncc.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MoTa", (object)ncc.MoTa ?? DBNull.Value);
            });
        }

        public void Update(NhaCungCap ncc)
        {
            ExecuteNonQuery(@"UPDATE NhaCungCap SET 
                                TenNCC = @TenNCC, 
                                DiaChi = @DiaChi, 
                                SoDienThoai = @SoDienThoai, 
                                Email = @Email, 
                                MoTa = @MoTa
                              WHERE MaNCC = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", ncc.MaNCC);
                cmd.Parameters.AddWithValue("@TenNCC", ncc.TenNCC);
                cmd.Parameters.AddWithValue("@DiaChi", (object)ncc.DiaChi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SoDienThoai", ncc.SoDienThoai);
                cmd.Parameters.AddWithValue("@Email", (object)ncc.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MoTa", (object)ncc.MoTa ?? DBNull.Value);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM NhaCungCap WHERE MaNCC = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
