using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class KhuyenMaiRepository
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

        private List<KhuyenMai> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<KhuyenMai>();
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
                            result.Add(new KhuyenMai
                            {
                                MaKhuyenMai = Convert.ToInt32(reader["MaKhuyenMai"]),
                                TenKhuyenMai = reader["TenKhuyenMai"].ToString(),
                                MaGiamGia = reader["MaGiamGia"] != DBNull.Value ? reader["MaGiamGia"].ToString() : null,
                                LoaiGiamGia = reader["LoaiGiamGia"].ToString(),
                                GiaTri = Convert.ToDecimal(reader["GiaTri"]),
                                NgayBatDau = Convert.ToDateTime(reader["NgayBatDau"]),
                                NgayKetThuc = Convert.ToDateTime(reader["NgayKetThuc"]),
                                TrangThai = reader["TrangThai"].ToString(),
                                MoTa = reader["MoTa"] != DBNull.Value ? reader["MoTa"].ToString() : null
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<KhuyenMai> GetAll() => ExecuteQuery("SELECT * FROM KhuyenMai");

        public KhuyenMai GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM KhuyenMai WHERE MaKhuyenMai = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(KhuyenMai km)
        {
            ExecuteNonQuery(@"INSERT INTO KhuyenMai 
            (TenKhuyenMai, MaGiamGia, LoaiGiamGia, GiaTri, NgayBatDau, NgayKetThuc, TrangThai, MoTa)
            VALUES (@TenKhuyenMai, @MaGiamGia, @LoaiGiamGia, @GiaTri, @NgayBatDau, @NgayKetThuc, @TrangThai, @MoTa)", cmd =>
            {
                cmd.Parameters.AddWithValue("@TenKhuyenMai", km.TenKhuyenMai);
                cmd.Parameters.AddWithValue("@MaGiamGia", (object)km.MaGiamGia ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LoaiGiamGia", km.LoaiGiamGia);
                cmd.Parameters.AddWithValue("@GiaTri", km.GiaTri);
                cmd.Parameters.AddWithValue("@NgayBatDau", km.NgayBatDau);
                cmd.Parameters.AddWithValue("@NgayKetThuc", km.NgayKetThuc);
                cmd.Parameters.AddWithValue("@TrangThai", (object)km.TrangThai ?? "Hoạt động");
                cmd.Parameters.AddWithValue("@MoTa", (object)km.MoTa ?? DBNull.Value);
            });
        }

        public void Update(KhuyenMai km)
        {
            ExecuteNonQuery(@"UPDATE KhuyenMai SET 
                TenKhuyenMai = @TenKhuyenMai,
                MaGiamGia = @MaGiamGia,
                LoaiGiamGia = @LoaiGiamGia,
                GiaTri = @GiaTri,
                NgayBatDau = @NgayBatDau,
                NgayKetThuc = @NgayKetThuc,
                TrangThai = @TrangThai,
                MoTa = @MoTa
            WHERE MaKhuyenMai = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", km.MaKhuyenMai);
                cmd.Parameters.AddWithValue("@TenKhuyenMai", km.TenKhuyenMai);
                cmd.Parameters.AddWithValue("@MaGiamGia", (object)km.MaGiamGia ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LoaiGiamGia", km.LoaiGiamGia);
                cmd.Parameters.AddWithValue("@GiaTri", km.GiaTri);
                cmd.Parameters.AddWithValue("@NgayBatDau", km.NgayBatDau);
                cmd.Parameters.AddWithValue("@NgayKetThuc", km.NgayKetThuc);
                cmd.Parameters.AddWithValue("@TrangThai", km.TrangThai);
                cmd.Parameters.AddWithValue("@MoTa", (object)km.MoTa ?? DBNull.Value);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM KhuyenMai WHERE MaKhuyenMai = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
