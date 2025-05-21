using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class TinhLuongRepository
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

        private List<TinhLuong> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<TinhLuong>();
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
                            result.Add(new TinhLuong
                            {
                                MaBangLuong = Convert.ToInt32(reader["MaBangLuong"]),
                                MaNV = reader["MaNV"].ToString(),
                                ThangNam = reader["ThangNam"].ToString(),
                                LuongCoBan = Convert.ToDecimal(reader["LuongCoBan"]),
                                PhuCap = Convert.ToDecimal(reader["PhuCap"]),
                                TienOT = Convert.ToDecimal(reader["TienOT"]),
                                TruPhat = Convert.ToDecimal(reader["TruPhat"]),
                                Thuong = Convert.ToDecimal(reader["Thuong"]),
                                TongThuNhap = Convert.ToDecimal(reader["TongThuNhap"]),
                                TrangThai = reader["TrangThai"].ToString(),
                                NgayThanhToan = reader["NgayThanhToan"] == DBNull.Value ? null : (DateTime?)reader["NgayThanhToan"]
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<TinhLuong> GetAll() => ExecuteQuery("SELECT * FROM TinhLuong");

        public TinhLuong GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM TinhLuong WHERE MaBangLuong = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(TinhLuong t)
        {
            ExecuteNonQuery(@"INSERT INTO TinhLuong 
                (MaNV, ThangNam, LuongCoBan, PhuCap, TienOT, TruPhat, Thuong, TrangThai, NgayThanhToan)
                VALUES (@MaNV, @ThangNam, @LuongCoBan, @PhuCap, @TienOT, @TruPhat, @Thuong, @TrangThai, @NgayThanhToan)", cmd =>
            {
                cmd.Parameters.AddWithValue("@MaNV", t.MaNV);
                cmd.Parameters.AddWithValue("@ThangNam", t.ThangNam);
                cmd.Parameters.AddWithValue("@LuongCoBan", t.LuongCoBan);
                cmd.Parameters.AddWithValue("@PhuCap", t.PhuCap);
                cmd.Parameters.AddWithValue("@TienOT", t.TienOT);
                cmd.Parameters.AddWithValue("@TruPhat", t.TruPhat);
                cmd.Parameters.AddWithValue("@Thuong", t.Thuong);
                cmd.Parameters.AddWithValue("@TrangThai", (object)t.TrangThai ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayThanhToan", (object)t.NgayThanhToan ?? DBNull.Value);
            });
        }

        public void Update(TinhLuong t)
        {
            ExecuteNonQuery(@"UPDATE TinhLuong 
                SET MaNV = @MaNV, ThangNam = @ThangNam, LuongCoBan = @LuongCoBan, PhuCap = @PhuCap, 
                    TienOT = @TienOT, TruPhat = @TruPhat, Thuong = @Thuong, 
                    TrangThai = @TrangThai, NgayThanhToan = @NgayThanhToan
                WHERE MaBangLuong = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", t.MaBangLuong);
                cmd.Parameters.AddWithValue("@MaNV", t.MaNV);
                cmd.Parameters.AddWithValue("@ThangNam", t.ThangNam);
                cmd.Parameters.AddWithValue("@LuongCoBan", t.LuongCoBan);
                cmd.Parameters.AddWithValue("@PhuCap", t.PhuCap);
                cmd.Parameters.AddWithValue("@TienOT", t.TienOT);
                cmd.Parameters.AddWithValue("@TruPhat", t.TruPhat);
                cmd.Parameters.AddWithValue("@Thuong", t.Thuong);
                cmd.Parameters.AddWithValue("@TrangThai", (object)t.TrangThai ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayThanhToan", (object)t.NgayThanhToan ?? DBNull.Value);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM TinhLuong WHERE MaBangLuong = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
