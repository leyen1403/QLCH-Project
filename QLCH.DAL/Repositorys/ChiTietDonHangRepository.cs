using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class ChiTietDonHangRepository
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

        private List<ChiTietDonHang> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<ChiTietDonHang>();
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
                            result.Add(new ChiTietDonHang
                            {
                                MaChiTietDH = Convert.ToInt32(reader["MaChiTietDH"]),
                                MaDonHang = Convert.ToInt32(reader["MaDonHang"]),
                                MaBienThe = Convert.ToInt32(reader["MaBienThe"]),
                                SoLuong = Convert.ToInt32(reader["SoLuong"]),
                                GiaBan = Convert.ToDecimal(reader["GiaBan"]),
                                ThanhTien = Convert.ToDecimal(reader["ThanhTien"])
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<ChiTietDonHang> GetAll() => ExecuteQuery("SELECT * FROM ChiTietDonHang");

        public ChiTietDonHang GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM ChiTietDonHang WHERE MaChiTietDH = @id",
                delegate (SqlCommand cmd)
                {
                    cmd.Parameters.AddWithValue("@id", id);
                });
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(ChiTietDonHang ct)
        {
            ExecuteNonQuery(@"INSERT INTO ChiTietDonHang (MaDonHang, MaBienThe, SoLuong, GiaBan)
                              VALUES (@MaDonHang, @MaBienThe, @SoLuong, @GiaBan)", cmd =>
            {
                cmd.Parameters.AddWithValue("@MaDonHang", ct.MaDonHang);
                cmd.Parameters.AddWithValue("@MaBienThe", ct.MaBienThe);
                cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                cmd.Parameters.AddWithValue("@GiaBan", ct.GiaBan);
            });
        }

        public void Update(ChiTietDonHang ct)
        {
            ExecuteNonQuery(@"UPDATE ChiTietDonHang SET 
                                MaDonHang = @MaDonHang,
                                MaBienThe = @MaBienThe,
                                SoLuong = @SoLuong,
                                GiaBan = @GiaBan
                              WHERE MaChiTietDH = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", ct.MaChiTietDH);
                cmd.Parameters.AddWithValue("@MaDonHang", ct.MaDonHang);
                cmd.Parameters.AddWithValue("@MaBienThe", ct.MaBienThe);
                cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                cmd.Parameters.AddWithValue("@GiaBan", ct.GiaBan);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM ChiTietDonHang WHERE MaChiTietDH = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
