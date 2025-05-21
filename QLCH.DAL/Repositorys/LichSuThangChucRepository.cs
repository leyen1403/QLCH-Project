using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class LichSuThangChucRepository
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

        private List<LichSuThangChuc> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var list = new List<LichSuThangChuc>();
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
                            list.Add(new LichSuThangChuc
                            {
                                MaLichSu = Convert.ToInt32(reader["MaLichSu"]),
                                MaNV = reader["MaNV"].ToString(),
                                MaChucVuMoi = Convert.ToInt32(reader["MaChucVuMoi"]),
                                NgayThangChuc = Convert.ToDateTime(reader["NgayThangChuc"]),
                                LyDo = reader["LyDo"]?.ToString()
                            });
                        }
                        return list;
                    }
                }
            }
        }

        public List<LichSuThangChuc> GetAll() => ExecuteQuery("SELECT * FROM LichSuThangChuc");

        public LichSuThangChuc GetById(int id)
        {
            var result = ExecuteQuery("SELECT * FROM LichSuThangChuc WHERE MaLichSu = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return result.Count > 0 ? result[0] : null;
        }

        public void Add(LichSuThangChuc ls)
        {
            string query = @"
                INSERT INTO LichSuThangChuc (MaNV, MaChucVuMoi, NgayThangChuc, LyDo)
                VALUES (@MaNV, @MaChucVuMoi, @NgayThangChuc, @LyDo)";
            ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@MaNV", ls.MaNV);
                cmd.Parameters.AddWithValue("@MaChucVuMoi", ls.MaChucVuMoi);
                cmd.Parameters.AddWithValue("@NgayThangChuc", ls.NgayThangChuc);
                cmd.Parameters.AddWithValue("@LyDo", (object)ls.LyDo ?? DBNull.Value);
            });
        }

        public void Update(LichSuThangChuc ls)
        {
            string query = @"
                UPDATE LichSuThangChuc SET
                    MaNV = @MaNV,
                    MaChucVuMoi = @MaChucVuMoi,
                    NgayThangChuc = @NgayThangChuc,
                    LyDo = @LyDo
                WHERE MaLichSu = @MaLichSu";
            ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@MaLichSu", ls.MaLichSu);
                cmd.Parameters.AddWithValue("@MaNV", ls.MaNV);
                cmd.Parameters.AddWithValue("@MaChucVuMoi", ls.MaChucVuMoi);
                cmd.Parameters.AddWithValue("@NgayThangChuc", ls.NgayThangChuc);
                cmd.Parameters.AddWithValue("@LyDo", (object)ls.LyDo ?? DBNull.Value);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM LichSuThangChuc WHERE MaLichSu = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
