using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class ChamCongRepository
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

        private List<ChamCong> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var list = new List<ChamCong>();
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
                            list.Add(new ChamCong
                            {
                                MaChamCong = Convert.ToInt32(reader["MaChamCong"]),
                                MaNV = reader["MaNV"].ToString(),
                                Ngay = Convert.ToDateTime(reader["Ngay"]),
                                GioVao = reader["GioVao"] == DBNull.Value ? (TimeSpan?)null : (TimeSpan)reader["GioVao"],
                                GioRa = reader["GioRa"] == DBNull.Value ? (TimeSpan?)null : (TimeSpan)reader["GioRa"],
                                SoGioLam = reader["SoGioLam"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(reader["SoGioLam"]),
                                LoaiCa = reader["LoaiCa"].ToString(),
                                TrangThai = reader["TrangThai"].ToString(),
                                GhiChu = reader["GhiChu"]?.ToString()
                            });
                        }
                        return list;
                    }
                }
            }
        }

        public List<ChamCong> GetAll() => ExecuteQuery("SELECT * FROM ChamCong");

        public ChamCong GetById(int id)
        {
            var result = ExecuteQuery("SELECT * FROM ChamCong WHERE MaChamCong = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return result.Count > 0 ? result[0] : null;
        }

        public void Add(ChamCong cc)
        {
            cc.SoGioLam = CalculateWorkingHours(cc.GioVao, cc.GioRa);

            string query = @"
                INSERT INTO ChamCong 
                (MaNV, Ngay, GioVao, GioRa, SoGioLam, LoaiCa, TrangThai, GhiChu)
                VALUES
                (@MaNV, @Ngay, @GioVao, @GioRa, @SoGioLam, @LoaiCa, @TrangThai, @GhiChu)";

            ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@MaNV", cc.MaNV);
                cmd.Parameters.AddWithValue("@Ngay", cc.Ngay);
                cmd.Parameters.AddWithValue("@GioVao", cc.GioVao.HasValue ? (object)cc.GioVao.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@GioRa", cc.GioRa.HasValue ? (object)cc.GioRa.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@SoGioLam", cc.SoGioLam.HasValue ? (object)cc.SoGioLam.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@LoaiCa", cc.LoaiCa);
                cmd.Parameters.AddWithValue("@TrangThai", cc.TrangThai ?? "Chưa duyệt");
                cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrEmpty(cc.GhiChu) ? (object)DBNull.Value : cc.GhiChu);
            });
        }

        public void Update(ChamCong cc)
        {
            cc.SoGioLam = CalculateWorkingHours(cc.GioVao, cc.GioRa);

            string query = @"
                UPDATE ChamCong SET
                    MaNV = @MaNV,
                    Ngay = @Ngay,
                    GioVao = @GioVao,
                    GioRa = @GioRa,
                    SoGioLam = @SoGioLam,
                    LoaiCa = @LoaiCa,
                    TrangThai = @TrangThai,
                    GhiChu = @GhiChu
                WHERE MaChamCong = @id";

            ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@id", cc.MaChamCong);
                cmd.Parameters.AddWithValue("@MaNV", cc.MaNV);
                cmd.Parameters.AddWithValue("@Ngay", cc.Ngay);
                cmd.Parameters.AddWithValue("@GioVao", cc.GioVao.HasValue ? (object)cc.GioVao.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@GioRa", cc.GioRa.HasValue ? (object)cc.GioRa.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@SoGioLam", cc.SoGioLam.HasValue ? (object)cc.SoGioLam.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@LoaiCa", cc.LoaiCa);
                cmd.Parameters.AddWithValue("@TrangThai", cc.TrangThai);
                cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrEmpty(cc.GhiChu) ? (object)DBNull.Value : cc.GhiChu);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM ChamCong WHERE MaChamCong = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }

        private decimal? CalculateWorkingHours(TimeSpan? vao, TimeSpan? ra)
        {
            if (vao.HasValue && ra.HasValue && ra > vao)
            {
                return (decimal)(ra.Value - vao.Value).TotalHours;
            }
            return null;
        }
    }
}
