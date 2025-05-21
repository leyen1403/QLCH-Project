using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class DangKyCaRepository
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

        private List<DangKyCa> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<DangKyCa>();
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
                            result.Add(new DangKyCa
                            {
                                MaDangKy = Convert.ToInt32(reader["MaDangKy"]),
                                MaNV = reader["MaNV"].ToString(),
                                MaCa = Convert.ToInt32(reader["MaCa"]),
                                Ngay = Convert.ToDateTime(reader["Ngay"]),
                                TrangThai = reader["TrangThai"].ToString(),
                                LyDo = reader["LyDo"]?.ToString()
                            });
                        }
                        return result;
                    }
                }
            }
        }

        public List<DangKyCa> GetAll() => ExecuteQuery("SELECT * FROM DangKyCa");

        public DangKyCa GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM DangKyCa WHERE MaDangKy = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(DangKyCa dk)
        {
            ExecuteNonQuery(@"INSERT INTO DangKyCa (MaNV, MaCa, Ngay, TrangThai, LyDo) 
                              VALUES (@MaNV, @MaCa, @Ngay, @TrangThai, @LyDo)", cmd =>
            {
                cmd.Parameters.AddWithValue("@MaNV", dk.MaNV);
                cmd.Parameters.AddWithValue("@MaCa", dk.MaCa);
                cmd.Parameters.AddWithValue("@Ngay", dk.Ngay);
                cmd.Parameters.AddWithValue("@TrangThai", (object)dk.TrangThai ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LyDo", (object)dk.LyDo ?? DBNull.Value);
            });
        }

        public void Update(DangKyCa dk)
        {
            ExecuteNonQuery(@"UPDATE DangKyCa 
                              SET MaNV = @MaNV, MaCa = @MaCa, Ngay = @Ngay, TrangThai = @TrangThai, LyDo = @LyDo 
                              WHERE MaDangKy = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", dk.MaDangKy);
                cmd.Parameters.AddWithValue("@MaNV", dk.MaNV);
                cmd.Parameters.AddWithValue("@MaCa", dk.MaCa);
                cmd.Parameters.AddWithValue("@Ngay", dk.Ngay);
                cmd.Parameters.AddWithValue("@TrangThai", (object)dk.TrangThai ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LyDo", (object)dk.LyDo ?? DBNull.Value);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM DangKyCa WHERE MaDangKy = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
