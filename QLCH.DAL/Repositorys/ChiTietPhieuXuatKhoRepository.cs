using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class ChiTietPhieuXuatKhoRepository
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

        private List<ChiTietPhieuXuatKho> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<ChiTietPhieuXuatKho>();
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
                            result.Add(new ChiTietPhieuXuatKho
                            {
                                MaChiTietXK = Convert.ToInt32(reader["MaChiTietXK"]),
                                MaPhieuXuat = Convert.ToInt32(reader["MaPhieuXuat"]),
                                MaBienThe = Convert.ToInt32(reader["MaBienThe"]),
                                SoLuong = Convert.ToInt32(reader["SoLuong"]),
                                GiaXuat = Convert.ToDecimal(reader["GiaXuat"]),
                                ThanhTien = Convert.ToDecimal(reader["ThanhTien"])
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<ChiTietPhieuXuatKho> GetAll() => ExecuteQuery("SELECT * FROM ChiTietPhieuXuatKho");

        public ChiTietPhieuXuatKho GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM ChiTietPhieuXuatKho WHERE MaChiTietXK = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(ChiTietPhieuXuatKho ct)
        {
            ExecuteNonQuery(@"INSERT INTO ChiTietPhieuXuatKho (MaPhieuXuat, MaBienThe, SoLuong, GiaXuat)
                              VALUES (@MaPhieuXuat, @MaBienThe, @SoLuong, @GiaXuat)", cmd =>
            {
                cmd.Parameters.AddWithValue("@MaPhieuXuat", ct.MaPhieuXuat);
                cmd.Parameters.AddWithValue("@MaBienThe", ct.MaBienThe);
                cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                cmd.Parameters.AddWithValue("@GiaXuat", ct.GiaXuat);
            });
        }

        public void Update(ChiTietPhieuXuatKho ct)
        {
            ExecuteNonQuery(@"UPDATE ChiTietPhieuXuatKho SET 
                MaPhieuXuat = @MaPhieuXuat,
                MaBienThe = @MaBienThe,
                SoLuong = @SoLuong,
                GiaXuat = @GiaXuat
                WHERE MaChiTietXK = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", ct.MaChiTietXK);
                cmd.Parameters.AddWithValue("@MaPhieuXuat", ct.MaPhieuXuat);
                cmd.Parameters.AddWithValue("@MaBienThe", ct.MaBienThe);
                cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                cmd.Parameters.AddWithValue("@GiaXuat", ct.GiaXuat);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM ChiTietPhieuXuatKho WHERE MaChiTietXK = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
