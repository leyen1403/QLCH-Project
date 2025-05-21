using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class BienTheSanPhamRepository
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

        private List<BienTheSanPham> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<BienTheSanPham>();
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
                            result.Add(new BienTheSanPham
                            {
                                MaBienThe = Convert.ToInt32(reader["MaBienThe"]),
                                MaSanPham = Convert.ToInt32(reader["MaSanPham"]),
                                TenBienThe = reader["TenBienThe"].ToString(),
                                MoTa = reader["MoTa"]?.ToString(),
                                GiaNhap = Convert.ToDecimal(reader["GiaNhap"]),
                                GiaBan = Convert.ToDecimal(reader["GiaBan"]),
                                TrangThai = reader["TrangThai"].ToString()
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<BienTheSanPham> GetAll() => ExecuteQuery("SELECT * FROM BienTheSanPham");

        public BienTheSanPham GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM BienTheSanPham WHERE MaBienThe = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(BienTheSanPham bts)
        {
            ExecuteNonQuery(@"INSERT INTO BienTheSanPham 
                (MaSanPham, TenBienThe, MoTa, GiaNhap, GiaBan, TrangThai) 
                VALUES (@MaSanPham, @TenBienThe, @MoTa, @GiaNhap, @GiaBan, @TrangThai)", cmd =>
            {
                cmd.Parameters.AddWithValue("@MaSanPham", bts.MaSanPham);
                cmd.Parameters.AddWithValue("@TenBienThe", bts.TenBienThe);
                cmd.Parameters.AddWithValue("@MoTa", (object)bts.MoTa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@GiaNhap", bts.GiaNhap);
                cmd.Parameters.AddWithValue("@GiaBan", bts.GiaBan);
                cmd.Parameters.AddWithValue("@TrangThai", (object)bts.TrangThai ?? "Hoạt động");
            });
        }

        public void Update(BienTheSanPham bts)
        {
            ExecuteNonQuery(@"UPDATE BienTheSanPham SET 
                MaSanPham = @MaSanPham, 
                TenBienThe = @TenBienThe, 
                MoTa = @MoTa, 
                GiaNhap = @GiaNhap, 
                GiaBan = @GiaBan, 
                TrangThai = @TrangThai 
                WHERE MaBienThe = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", bts.MaBienThe);
                cmd.Parameters.AddWithValue("@MaSanPham", bts.MaSanPham);
                cmd.Parameters.AddWithValue("@TenBienThe", bts.TenBienThe);
                cmd.Parameters.AddWithValue("@MoTa", (object)bts.MoTa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@GiaNhap", bts.GiaNhap);
                cmd.Parameters.AddWithValue("@GiaBan", bts.GiaBan);
                cmd.Parameters.AddWithValue("@TrangThai", (object)bts.TrangThai ?? "Hoạt động");
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM BienTheSanPham WHERE MaBienThe = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
