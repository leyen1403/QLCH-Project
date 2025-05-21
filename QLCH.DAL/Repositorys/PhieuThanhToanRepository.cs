using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class PhieuThanhToanRepository
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

        private List<PhieuThanhToan> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<PhieuThanhToan>();
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
                            result.Add(new PhieuThanhToan
                            {
                                MaPhieuThanhToan = Convert.ToInt32(reader["MaPhieuThanhToan"]),
                                MaDonHang = Convert.ToInt32(reader["MaDonHang"]),
                                NgayThanhToan = Convert.ToDateTime(reader["NgayThanhToan"]),
                                SoTienThanhToan = Convert.ToDecimal(reader["SoTienThanhToan"]),
                                PhuongThuc = reader["PhuongThuc"].ToString(),
                                TrangThai = reader["TrangThai"].ToString(),
                                GhiChu = reader["GhiChu"] != DBNull.Value ? reader["GhiChu"].ToString() : null
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<PhieuThanhToan> GetAll() => ExecuteQuery("SELECT * FROM PhieuThanhToan");

        public PhieuThanhToan GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM PhieuThanhToan WHERE MaPhieuThanhToan = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(PhieuThanhToan pt)
        {
            ExecuteNonQuery(@"INSERT INTO PhieuThanhToan 
                (MaDonHang, NgayThanhToan, SoTienThanhToan, PhuongThuc, TrangThai, GhiChu)
                VALUES (@MaDonHang, @NgayThanhToan, @SoTienThanhToan, @PhuongThuc, @TrangThai, @GhiChu)", cmd =>
            {
                cmd.Parameters.AddWithValue("@MaDonHang", pt.MaDonHang);
                cmd.Parameters.AddWithValue("@NgayThanhToan", pt.NgayThanhToan == default(DateTime) ? DateTime.Now : pt.NgayThanhToan);
                cmd.Parameters.AddWithValue("@SoTienThanhToan", pt.SoTienThanhToan);
                cmd.Parameters.AddWithValue("@PhuongThuc", pt.PhuongThuc);
                cmd.Parameters.AddWithValue("@TrangThai", pt.TrangThai ?? "Đã thanh toán");
                cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrEmpty(pt.GhiChu) ? DBNull.Value : (object)pt.GhiChu);
            });
        }

        public void Update(PhieuThanhToan pt)
        {
            ExecuteNonQuery(@"UPDATE PhieuThanhToan SET 
                MaDonHang = @MaDonHang,
                NgayThanhToan = @NgayThanhToan,
                SoTienThanhToan = @SoTienThanhToan,
                PhuongThuc = @PhuongThuc,
                TrangThai = @TrangThai,
                GhiChu = @GhiChu
                WHERE MaPhieuThanhToan = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", pt.MaPhieuThanhToan);
                cmd.Parameters.AddWithValue("@MaDonHang", pt.MaDonHang);
                cmd.Parameters.AddWithValue("@NgayThanhToan", pt.NgayThanhToan);
                cmd.Parameters.AddWithValue("@SoTienThanhToan", pt.SoTienThanhToan);
                cmd.Parameters.AddWithValue("@PhuongThuc", pt.PhuongThuc);
                cmd.Parameters.AddWithValue("@TrangThai", pt.TrangThai);
                cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrEmpty(pt.GhiChu) ? DBNull.Value : (object)pt.GhiChu);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM PhieuThanhToan WHERE MaPhieuThanhToan = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
