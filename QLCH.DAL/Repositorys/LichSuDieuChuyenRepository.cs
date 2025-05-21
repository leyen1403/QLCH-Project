using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class LichSuDieuChuyenRepository
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

        private List<LichSuDieuChuyen> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var list = new List<LichSuDieuChuyen>();
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
                            list.Add(new LichSuDieuChuyen
                            {
                                MaDieuChuyen = Convert.ToInt32(reader["MaDieuChuyen"]),
                                MaNV = reader["MaNV"].ToString(),
                                MaPhongBanMoi = Convert.ToInt32(reader["MaPhongBanMoi"]),
                                MaCuaHangMoi = Convert.ToInt32(reader["MaCuaHangMoi"]),
                                NgayDieuChuyen = Convert.ToDateTime(reader["NgayDieuChuyen"]),
                                LyDo = reader["LyDo"]?.ToString()
                            });
                        }
                        return list;
                    }
                }
            }
        }

        public List<LichSuDieuChuyen> GetAll() => ExecuteQuery("SELECT * FROM LichSuDieuChuyen");

        public LichSuDieuChuyen GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM LichSuDieuChuyen WHERE MaDieuChuyen = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(LichSuDieuChuyen ls)
        {
            string query = @"
                INSERT INTO LichSuDieuChuyen (MaNV, MaPhongBanMoi, MaCuaHangMoi, NgayDieuChuyen, LyDo)
                VALUES (@MaNV, @MaPhongBanMoi, @MaCuaHangMoi, @NgayDieuChuyen, @LyDo)";
            ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@MaNV", ls.MaNV);
                cmd.Parameters.AddWithValue("@MaPhongBanMoi", ls.MaPhongBanMoi);
                cmd.Parameters.AddWithValue("@MaCuaHangMoi", ls.MaCuaHangMoi);
                cmd.Parameters.AddWithValue("@NgayDieuChuyen", ls.NgayDieuChuyen);
                cmd.Parameters.AddWithValue("@LyDo", (object)ls.LyDo ?? DBNull.Value);
            });
        }

        public void Update(LichSuDieuChuyen ls)
        {
            string query = @"
                UPDATE LichSuDieuChuyen SET
                    MaNV = @MaNV,
                    MaPhongBanMoi = @MaPhongBanMoi,
                    MaCuaHangMoi = @MaCuaHangMoi,
                    NgayDieuChuyen = @NgayDieuChuyen,
                    LyDo = @LyDo
                WHERE MaDieuChuyen = @id";
            ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@id", ls.MaDieuChuyen);
                cmd.Parameters.AddWithValue("@MaNV", ls.MaNV);
                cmd.Parameters.AddWithValue("@MaPhongBanMoi", ls.MaPhongBanMoi);
                cmd.Parameters.AddWithValue("@MaCuaHangMoi", ls.MaCuaHangMoi);
                cmd.Parameters.AddWithValue("@NgayDieuChuyen", ls.NgayDieuChuyen);
                cmd.Parameters.AddWithValue("@LyDo", (object)ls.LyDo ?? DBNull.Value);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM LichSuDieuChuyen WHERE MaDieuChuyen = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
