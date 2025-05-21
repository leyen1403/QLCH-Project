// CuaHangRepository.cs
using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class CuaHangRepository
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
                        throw new Exception($"Lỗi khi thực thi SQL: {ex.Message}");
                    }
                }
            }
        }

        private List<CuaHang> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<CuaHang>();
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
                            result.Add(new CuaHang
                            {
                                MaCuaHang = Convert.ToInt32(reader["MaCuaHang"]),
                                TenCuaHang = reader["TenCuaHang"].ToString(),
                                DiaChi = reader["DiaChi"].ToString(),
                                SoDienThoai = reader["SoDienThoai"].ToString(),
                                MoTa = reader["MoTa"]?.ToString()
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<CuaHang> GetAll() => ExecuteQuery("SELECT * FROM CuaHang");

        public CuaHang GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM CuaHang WHERE MaCuaHang = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(CuaHang ch)
        {
            string query = @"
                INSERT INTO CuaHang (TenCuaHang, DiaChi, SoDienThoai, MoTa)
                VALUES (@TenCuaHang, @DiaChi, @SoDienThoai, @MoTa)";
            ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@TenCuaHang", ch.TenCuaHang);
                cmd.Parameters.AddWithValue("@DiaChi", ch.DiaChi);
                cmd.Parameters.AddWithValue("@SoDienThoai", ch.SoDienThoai);
                cmd.Parameters.AddWithValue("@MoTa", (object)ch.MoTa ?? DBNull.Value);
            });
        }

        public void Update(CuaHang ch)
        {
            string query = @"
                UPDATE CuaHang SET
                    TenCuaHang = @TenCuaHang,
                    DiaChi = @DiaChi,
                    SoDienThoai = @SoDienThoai,
                    MoTa = @MoTa
                WHERE MaCuaHang = @id";
            ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@id", ch.MaCuaHang);
                cmd.Parameters.AddWithValue("@TenCuaHang", ch.TenCuaHang);
                cmd.Parameters.AddWithValue("@DiaChi", ch.DiaChi);
                cmd.Parameters.AddWithValue("@SoDienThoai", ch.SoDienThoai);
                cmd.Parameters.AddWithValue("@MoTa", (object)ch.MoTa ?? DBNull.Value);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM CuaHang WHERE MaCuaHang = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
