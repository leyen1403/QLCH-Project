using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class PhieuNhapHangRepository
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

        private List<PhieuNhapHang> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<PhieuNhapHang>();
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
                            result.Add(new PhieuNhapHang
                            {
                                MaPhieuNhap = Convert.ToInt32(reader["MaPhieuNhap"]),
                                MaKho = Convert.ToInt32(reader["MaKho"]),
                                NgayNhap = Convert.ToDateTime(reader["NgayNhap"]),
                                NguoiNhap = reader["NguoiNhap"].ToString(),
                                TongTien = Convert.ToDecimal(reader["TongTien"]),
                                GhiChu = reader["GhiChu"]?.ToString()
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<PhieuNhapHang> GetAll() => ExecuteQuery("SELECT * FROM PhieuNhapHang");

        public PhieuNhapHang GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM PhieuNhapHang WHERE MaPhieuNhap = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(PhieuNhapHang ph)
        {
            ExecuteNonQuery(@"INSERT INTO PhieuNhapHang (MaKho, NgayNhap, NguoiNhap, TongTien, GhiChu)
                              VALUES (@MaKho, @NgayNhap, @NguoiNhap, @TongTien, @GhiChu)", cmd =>
            {
                cmd.Parameters.AddWithValue("@MaKho", ph.MaKho);
                cmd.Parameters.AddWithValue("@NgayNhap", ph.NgayNhap);
                cmd.Parameters.AddWithValue("@NguoiNhap", ph.NguoiNhap);
                cmd.Parameters.AddWithValue("@TongTien", ph.TongTien);
                cmd.Parameters.AddWithValue("@GhiChu", (object)ph.GhiChu ?? DBNull.Value);
            });
        }

        public void Update(PhieuNhapHang ph)
        {
            ExecuteNonQuery(@"UPDATE PhieuNhapHang SET 
                                MaKho = @MaKho,
                                NgayNhap = @NgayNhap,
                                NguoiNhap = @NguoiNhap,
                                TongTien = @TongTien,
                                GhiChu = @GhiChu
                              WHERE MaPhieuNhap = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", ph.MaPhieuNhap);
                cmd.Parameters.AddWithValue("@MaKho", ph.MaKho);
                cmd.Parameters.AddWithValue("@NgayNhap", ph.NgayNhap);
                cmd.Parameters.AddWithValue("@NguoiNhap", ph.NguoiNhap);
                cmd.Parameters.AddWithValue("@TongTien", ph.TongTien);
                cmd.Parameters.AddWithValue("@GhiChu", (object)ph.GhiChu ?? DBNull.Value);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM PhieuNhapHang WHERE MaPhieuNhap = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
