using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class BaoCaoTonKhoRepository
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

        private List<BaoCaoTonKho> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<BaoCaoTonKho>();
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
                            result.Add(new BaoCaoTonKho
                            {
                                MaBaoCaoTonKho = Convert.ToInt32(reader["MaBaoCaoTonKho"]),
                                MaKho = Convert.ToInt32(reader["MaKho"]),
                                ThoiGian = Convert.ToDateTime(reader["ThoiGian"]),
                                TongSanPham = Convert.ToInt32(reader["TongSanPham"]),
                                TongSoLuong = Convert.ToInt32(reader["TongSoLuong"]),
                                GhiChu = reader["GhiChu"] != DBNull.Value ? reader["GhiChu"].ToString() : null
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<BaoCaoTonKho> GetAll() => ExecuteQuery("SELECT * FROM BaoCaoTonKho");

        public BaoCaoTonKho GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM BaoCaoTonKho WHERE MaBaoCaoTonKho = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(BaoCaoTonKho bc)
        {
            ExecuteNonQuery(@"INSERT INTO BaoCaoTonKho (MaKho, ThoiGian, TongSanPham, TongSoLuong, GhiChu)
                              VALUES (@MaKho, @ThoiGian, @TongSanPham, @TongSoLuong, @GhiChu)", cmd =>
            {
                cmd.Parameters.AddWithValue("@MaKho", bc.MaKho);
                cmd.Parameters.AddWithValue("@ThoiGian", bc.ThoiGian == default(DateTime) ? DateTime.Now : bc.ThoiGian);
                cmd.Parameters.AddWithValue("@TongSanPham", bc.TongSanPham);
                cmd.Parameters.AddWithValue("@TongSoLuong", bc.TongSoLuong);
                cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrEmpty(bc.GhiChu) ? DBNull.Value : (object)bc.GhiChu);
            });
        }

        public void Update(BaoCaoTonKho bc)
        {
            ExecuteNonQuery(@"UPDATE BaoCaoTonKho SET 
                MaKho = @MaKho,
                ThoiGian = @ThoiGian,
                TongSanPham = @TongSanPham,
                TongSoLuong = @TongSoLuong,
                GhiChu = @GhiChu
                WHERE MaBaoCaoTonKho = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", bc.MaBaoCaoTonKho);
                cmd.Parameters.AddWithValue("@MaKho", bc.MaKho);
                cmd.Parameters.AddWithValue("@ThoiGian", bc.ThoiGian);
                cmd.Parameters.AddWithValue("@TongSanPham", bc.TongSanPham);
                cmd.Parameters.AddWithValue("@TongSoLuong", bc.TongSoLuong);
                cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrEmpty(bc.GhiChu) ? DBNull.Value : (object)bc.GhiChu);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM BaoCaoTonKho WHERE MaBaoCaoTonKho = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
