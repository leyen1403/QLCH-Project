using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class DonHangRepository
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

        private List<DonHang> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<DonHang>();
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
                            result.Add(new DonHang
                            {
                                MaDonHang = Convert.ToInt32(reader["MaDonHang"]),
                                MaKhachHang = reader["MaKhachHang"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["MaKhachHang"]),
                                NgayDat = Convert.ToDateTime(reader["NgayDat"]),
                                TongTien = Convert.ToDecimal(reader["TongTien"]),
                                GiamGia = Convert.ToDecimal(reader["GiamGia"]),
                                TongTienSauGiam = Convert.ToDecimal(reader["TongTienSauGiam"]),
                                TrangThai = reader["TrangThai"].ToString(),
                                GhiChu = reader["GhiChu"]?.ToString()
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<DonHang> GetAll() => ExecuteQuery("SELECT * FROM DonHang");

        public DonHang GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM DonHang WHERE MaDonHang = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(DonHang dh)
        {
            ExecuteNonQuery(@"INSERT INTO DonHang (MaKhachHang, NgayDat, TongTien, GiamGia, TrangThai, GhiChu)
                              VALUES (@MaKhachHang, @NgayDat, @TongTien, @GiamGia, @TrangThai, @GhiChu)", cmd =>
            {
                cmd.Parameters.AddWithValue("@MaKhachHang", dh.MaKhachHang != null ? (object)dh.MaKhachHang : DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayDat", dh.NgayDat == default ? DateTime.Now : dh.NgayDat);
                cmd.Parameters.AddWithValue("@TongTien", dh.TongTien);
                cmd.Parameters.AddWithValue("@GiamGia", dh.GiamGia);
                cmd.Parameters.AddWithValue("@TrangThai", dh.TrangThai ?? "Chưa thanh toán");
                cmd.Parameters.AddWithValue("@GhiChu", (object)dh.GhiChu ?? DBNull.Value);
            });
        }

        public void Update(DonHang dh)
        {
            ExecuteNonQuery(@"UPDATE DonHang SET 
                                MaKhachHang = @MaKhachHang,
                                NgayDat = @NgayDat,
                                TongTien = @TongTien,
                                GiamGia = @GiamGia,
                                TrangThai = @TrangThai,
                                GhiChu = @GhiChu
                              WHERE MaDonHang = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", dh.MaDonHang);
                cmd.Parameters.AddWithValue("@MaKhachHang", dh.MaKhachHang != null ? (object)dh.MaKhachHang : DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayDat", dh.NgayDat);
                cmd.Parameters.AddWithValue("@TongTien", dh.TongTien);
                cmd.Parameters.AddWithValue("@GiamGia", dh.GiamGia);
                cmd.Parameters.AddWithValue("@TrangThai", dh.TrangThai);
                cmd.Parameters.AddWithValue("@GhiChu", (object)dh.GhiChu ?? DBNull.Value);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM DonHang WHERE MaDonHang = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
