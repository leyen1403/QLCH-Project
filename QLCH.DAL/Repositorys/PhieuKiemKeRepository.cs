using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class PhieuKiemKeRepository
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

        private List<PhieuKiemKe> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<PhieuKiemKe>();
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
                            result.Add(new PhieuKiemKe
                            {
                                MaPhieuKiemKe = Convert.ToInt32(reader["MaPhieuKiemKe"]),
                                MaKho = Convert.ToInt32(reader["MaKho"]),
                                NgayKiemKe = Convert.ToDateTime(reader["NgayKiemKe"]),
                                NguoiKiemKe = reader["NguoiKiemKe"].ToString(),
                                GhiChu = reader["GhiChu"] != DBNull.Value ? reader["GhiChu"].ToString() : null
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<PhieuKiemKe> GetAll() => ExecuteQuery("SELECT * FROM PhieuKiemKe");

        public PhieuKiemKe GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM PhieuKiemKe WHERE MaPhieuKiemKe = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(PhieuKiemKe pk)
        {
            ExecuteNonQuery(@"INSERT INTO PhieuKiemKe (MaKho, NgayKiemKe, NguoiKiemKe, GhiChu)
                              VALUES (@MaKho, @NgayKiemKe, @NguoiKiemKe, @GhiChu)", cmd =>
            {
                cmd.Parameters.AddWithValue("@MaKho", pk.MaKho);
                cmd.Parameters.AddWithValue("@NgayKiemKe", pk.NgayKiemKe == default(DateTime) ? DateTime.Now : pk.NgayKiemKe);
                cmd.Parameters.AddWithValue("@NguoiKiemKe", pk.NguoiKiemKe);
                cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrEmpty(pk.GhiChu) ? DBNull.Value : (object)pk.GhiChu);
            });
        }

        public void Update(PhieuKiemKe pk)
        {
            ExecuteNonQuery(@"UPDATE PhieuKiemKe SET 
                MaKho = @MaKho,
                NgayKiemKe = @NgayKiemKe,
                NguoiKiemKe = @NguoiKiemKe,
                GhiChu = @GhiChu
                WHERE MaPhieuKiemKe = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", pk.MaPhieuKiemKe);
                cmd.Parameters.AddWithValue("@MaKho", pk.MaKho);
                cmd.Parameters.AddWithValue("@NgayKiemKe", pk.NgayKiemKe);
                cmd.Parameters.AddWithValue("@NguoiKiemKe", pk.NguoiKiemKe);
                cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrEmpty(pk.GhiChu) ? DBNull.Value : (object)pk.GhiChu);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM PhieuKiemKe WHERE MaPhieuKiemKe = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
