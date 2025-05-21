using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class ChiTietPhieuKiemKeRepository
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
                        throw new Exception("Lỗi SQL: " + ex.Message);
                    }
                }
            }
        }

        private List<ChiTietPhieuKiemKe> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<ChiTietPhieuKiemKe>();
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    if (paramMap != null) paramMap(cmd);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new ChiTietPhieuKiemKe
                            {
                                MaChiTietKK = Convert.ToInt32(reader["MaChiTietKK"]),
                                MaPhieuKiemKe = Convert.ToInt32(reader["MaPhieuKiemKe"]),
                                MaBienThe = Convert.ToInt32(reader["MaBienThe"]),
                                SoLuongThucTe = Convert.ToInt32(reader["SoLuongThucTe"]),
                                SoLuongChenhLech = reader["SoLuongChenhLech"] != DBNull.Value ? (int?)Convert.ToInt32(reader["SoLuongChenhLech"]) : null
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<ChiTietPhieuKiemKe> GetAll() => ExecuteQuery("SELECT * FROM ChiTietPhieuKiemKe");

        public ChiTietPhieuKiemKe GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM ChiTietPhieuKiemKe WHERE MaChiTietKK = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(ChiTietPhieuKiemKe ct)
        {
            ExecuteNonQuery(@"INSERT INTO ChiTietPhieuKiemKe (MaPhieuKiemKe, MaBienThe, SoLuongThucTe, SoLuongChenhLech)
                              VALUES (@MaPhieuKiemKe, @MaBienThe, @SoLuongThucTe, @SoLuongChenhLech)", cmd =>
            {
                cmd.Parameters.AddWithValue("@MaPhieuKiemKe", ct.MaPhieuKiemKe);
                cmd.Parameters.AddWithValue("@MaBienThe", ct.MaBienThe);
                cmd.Parameters.AddWithValue("@SoLuongThucTe", ct.SoLuongThucTe);
                cmd.Parameters.AddWithValue("@SoLuongChenhLech", ct.SoLuongChenhLech.HasValue ? (object)ct.SoLuongChenhLech.Value : DBNull.Value);
            });
        }

        public void Update(ChiTietPhieuKiemKe ct)
        {
            ExecuteNonQuery(@"UPDATE ChiTietPhieuKiemKe SET 
                MaPhieuKiemKe = @MaPhieuKiemKe,
                MaBienThe = @MaBienThe,
                SoLuongThucTe = @SoLuongThucTe,
                SoLuongChenhLech = @SoLuongChenhLech
                WHERE MaChiTietKK = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", ct.MaChiTietKK);
                cmd.Parameters.AddWithValue("@MaPhieuKiemKe", ct.MaPhieuKiemKe);
                cmd.Parameters.AddWithValue("@MaBienThe", ct.MaBienThe);
                cmd.Parameters.AddWithValue("@SoLuongThucTe", ct.SoLuongThucTe);
                cmd.Parameters.AddWithValue("@SoLuongChenhLech", ct.SoLuongChenhLech.HasValue ? (object)ct.SoLuongChenhLech.Value : DBNull.Value);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM ChiTietPhieuKiemKe WHERE MaChiTietKK = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
