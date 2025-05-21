using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class ChiTietKhuyenMaiRepository
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

        private List<ChiTietKhuyenMai> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<ChiTietKhuyenMai>();
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
                            result.Add(new ChiTietKhuyenMai
                            {
                                MaChiTietKM = Convert.ToInt32(reader["MaChiTietKM"]),
                                MaDonHang = Convert.ToInt32(reader["MaDonHang"]),
                                MaKhuyenMai = Convert.ToInt32(reader["MaKhuyenMai"]),
                                GiaTriGiam = Convert.ToDecimal(reader["GiaTriGiam"])
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<ChiTietKhuyenMai> GetAll() => ExecuteQuery("SELECT * FROM ChiTietKhuyenMai");

        public ChiTietKhuyenMai GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM ChiTietKhuyenMai WHERE MaChiTietKM = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(ChiTietKhuyenMai ct)
        {
            ExecuteNonQuery(@"INSERT INTO ChiTietKhuyenMai (MaDonHang, MaKhuyenMai, GiaTriGiam)
                              VALUES (@MaDonHang, @MaKhuyenMai, @GiaTriGiam)", cmd =>
            {
                cmd.Parameters.AddWithValue("@MaDonHang", ct.MaDonHang);
                cmd.Parameters.AddWithValue("@MaKhuyenMai", ct.MaKhuyenMai);
                cmd.Parameters.AddWithValue("@GiaTriGiam", ct.GiaTriGiam);
            });
        }

        public void Update(ChiTietKhuyenMai ct)
        {
            ExecuteNonQuery(@"UPDATE ChiTietKhuyenMai SET 
                                MaDonHang = @MaDonHang,
                                MaKhuyenMai = @MaKhuyenMai,
                                GiaTriGiam = @GiaTriGiam
                              WHERE MaChiTietKM = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", ct.MaChiTietKM);
                cmd.Parameters.AddWithValue("@MaDonHang", ct.MaDonHang);
                cmd.Parameters.AddWithValue("@MaKhuyenMai", ct.MaKhuyenMai);
                cmd.Parameters.AddWithValue("@GiaTriGiam", ct.GiaTriGiam);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM ChiTietKhuyenMai WHERE MaChiTietKM = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
