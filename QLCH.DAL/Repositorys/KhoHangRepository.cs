using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class KhoHangRepository
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

        private List<KhoHang> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<KhoHang>();
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
                            result.Add(new KhoHang
                            {
                                MaKho = Convert.ToInt32(reader["MaKho"]),
                                MaCuaHang = Convert.ToInt32(reader["MaCuaHang"]),
                                TenKho = reader["TenKho"].ToString(),
                                DiaChi = reader["DiaChi"].ToString(),
                                SoDienThoai = reader["SoDienThoai"].ToString()
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<KhoHang> GetAll() => ExecuteQuery("SELECT * FROM KhoHang");

        public KhoHang GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM KhoHang WHERE MaKho = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(KhoHang kho)
        {
            ExecuteNonQuery(@"INSERT INTO KhoHang (MaCuaHang, TenKho, DiaChi, SoDienThoai)
                              VALUES (@MaCuaHang, @TenKho, @DiaChi, @SoDienThoai)", cmd =>
            {
                cmd.Parameters.AddWithValue("@MaCuaHang", kho.MaCuaHang);
                cmd.Parameters.AddWithValue("@TenKho", kho.TenKho);
                cmd.Parameters.AddWithValue("@DiaChi", kho.DiaChi);
                cmd.Parameters.AddWithValue("@SoDienThoai", kho.SoDienThoai);
            });
        }

        public void Update(KhoHang kho)
        {
            ExecuteNonQuery(@"UPDATE KhoHang SET 
                                MaCuaHang = @MaCuaHang,
                                TenKho = @TenKho,
                                DiaChi = @DiaChi,
                                SoDienThoai = @SoDienThoai
                              WHERE MaKho = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", kho.MaKho);
                cmd.Parameters.AddWithValue("@MaCuaHang", kho.MaCuaHang);
                cmd.Parameters.AddWithValue("@TenKho", kho.TenKho);
                cmd.Parameters.AddWithValue("@DiaChi", kho.DiaChi);
                cmd.Parameters.AddWithValue("@SoDienThoai", kho.SoDienThoai);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM KhoHang WHERE MaKho = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
