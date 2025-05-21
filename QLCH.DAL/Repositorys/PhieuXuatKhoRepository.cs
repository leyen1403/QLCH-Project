using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class PhieuXuatKhoRepository
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

        private List<PhieuXuatKho> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<PhieuXuatKho>();
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
                            result.Add(new PhieuXuatKho
                            {
                                MaPhieuXuat = Convert.ToInt32(reader["MaPhieuXuat"]),
                                MaKhoXuat = Convert.ToInt32(reader["MaKhoXuat"]),
                                MaKhoNhan = reader["MaKhoNhan"] != DBNull.Value ? (int?)Convert.ToInt32(reader["MaKhoNhan"]) : null,
                                MaNCC = reader["MaNCC"] != DBNull.Value ? (int?)Convert.ToInt32(reader["MaNCC"]) : null,
                                NgayXuat = Convert.ToDateTime(reader["NgayXuat"]),
                                LoaiXuat = reader["LoaiXuat"].ToString(),
                                TongTien = Convert.ToDecimal(reader["TongTien"]),
                                GhiChu = reader["GhiChu"] != DBNull.Value ? reader["GhiChu"].ToString() : null
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<PhieuXuatKho> GetAll() => ExecuteQuery("SELECT * FROM PhieuXuatKho");

        public PhieuXuatKho GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM PhieuXuatKho WHERE MaPhieuXuat = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(PhieuXuatKho px)
        {
            ExecuteNonQuery(@"INSERT INTO PhieuXuatKho 
                (MaKhoXuat, MaKhoNhan, MaNCC, NgayXuat, LoaiXuat, TongTien, GhiChu)
                VALUES (@MaKhoXuat, @MaKhoNhan, @MaNCC, @NgayXuat, @LoaiXuat, @TongTien, @GhiChu)", cmd =>
            {
                cmd.Parameters.AddWithValue("@MaKhoXuat", px.MaKhoXuat);
                cmd.Parameters.AddWithValue("@MaKhoNhan", px.MaKhoNhan != null ? (object)px.MaKhoNhan : DBNull.Value);
                cmd.Parameters.AddWithValue("@MaNCC", px.MaNCC != null ? (object)px.MaNCC : DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayXuat", px.NgayXuat == default(DateTime) ? DateTime.Now : px.NgayXuat);
                cmd.Parameters.AddWithValue("@LoaiXuat", px.LoaiXuat);
                cmd.Parameters.AddWithValue("@TongTien", px.TongTien);
                cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrEmpty(px.GhiChu) ? DBNull.Value : (object)px.GhiChu);
            });
        }

        public void Update(PhieuXuatKho px)
        {
            ExecuteNonQuery(@"UPDATE PhieuXuatKho SET 
                MaKhoXuat = @MaKhoXuat,
                MaKhoNhan = @MaKhoNhan,
                MaNCC = @MaNCC,
                NgayXuat = @NgayXuat,
                LoaiXuat = @LoaiXuat,
                TongTien = @TongTien,
                GhiChu = @GhiChu
            WHERE MaPhieuXuat = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", px.MaPhieuXuat);
                cmd.Parameters.AddWithValue("@MaKhoXuat", px.MaKhoXuat);
                cmd.Parameters.AddWithValue("@MaKhoNhan", px.MaKhoNhan != null ? (object)px.MaKhoNhan : DBNull.Value);
                cmd.Parameters.AddWithValue("@MaNCC", px.MaNCC != null ? (object)px.MaNCC : DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayXuat", px.NgayXuat);
                cmd.Parameters.AddWithValue("@LoaiXuat", px.LoaiXuat);
                cmd.Parameters.AddWithValue("@TongTien", px.TongTien);
                cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrEmpty(px.GhiChu) ? DBNull.Value : (object)px.GhiChu);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM PhieuXuatKho WHERE MaPhieuXuat = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
