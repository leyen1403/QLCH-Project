// HopDongLaoDongRepository.cs
using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QLCH.DAL.Repositorys
{
    public class HopDongLaoDongRepository
    {
        private readonly string _connectionString = GlobalVariables.ConnectionString;

        private void ExecuteNonQueryWithTransaction(string query, Action<SqlCommand> parameterMapper)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand(query, connection, transaction))
                        {
                            parameterMapper(command);
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Lỗi khi thực thi SQL: {ex.Message}");
                    }
                }
            }
        }

        private List<HopDongLaoDong> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<HopDongLaoDong>();
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
                            result.Add(MapToModel(reader));
                        }
                    }
                }
            }
            return result;
        }

        private HopDongLaoDong MapToModel(SqlDataReader reader)
        {
            return new HopDongLaoDong
            {
                MaHopDong = Convert.ToInt32(reader["MaHopDong"]),
                MaNV = reader["MaNV"].ToString(),
                LoaiHopDong = reader["LoaiHopDong"].ToString(),
                NgayKy = Convert.ToDateTime(reader["NgayKy"]),
                NgayHieuLuc = Convert.ToDateTime(reader["NgayHieuLuc"]),
                NgayKetThuc = reader["NgayKetThuc"] == DBNull.Value ? null : (DateTime?)reader["NgayKetThuc"],
                LuongCoBan = Convert.ToDecimal(reader["LuongCoBan"]),
                ThoiHanHD = Convert.ToInt32(reader["ThoiHanHD"]),
                TrangThai = reader["TrangThai"].ToString()
            };
        }

        public List<HopDongLaoDong> GetAll() => ExecuteQuery("SELECT * FROM HopDongLaoDong");

        public HopDongLaoDong GetById(int id)
        {
            var result = ExecuteQuery("SELECT * FROM HopDongLaoDong WHERE MaHopDong = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return result.Count > 0 ? result[0] : null;
        }

        public void Add(HopDongLaoDong hd)
        {
            string query = @"
                INSERT INTO HopDongLaoDong 
                (MaNV, LoaiHopDong, NgayKy, NgayHieuLuc, NgayKetThuc, LuongCoBan, ThoiHanHD, TrangThai)
                VALUES 
                (@MaNV, @LoaiHopDong, @NgayKy, @NgayHieuLuc, @NgayKetThuc, @LuongCoBan, @ThoiHanHD, @TrangThai)";
            ExecuteNonQueryWithTransaction(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@MaNV", hd.MaNV);
                cmd.Parameters.AddWithValue("@LoaiHopDong", hd.LoaiHopDong);
                cmd.Parameters.AddWithValue("@NgayKy", hd.NgayKy);
                cmd.Parameters.AddWithValue("@NgayHieuLuc", hd.NgayHieuLuc);
                cmd.Parameters.AddWithValue("@NgayKetThuc", hd.NgayKetThuc.HasValue ? (object)hd.NgayKetThuc.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@LuongCoBan", hd.LuongCoBan);
                cmd.Parameters.AddWithValue("@ThoiHanHD", hd.ThoiHanHD);
                cmd.Parameters.AddWithValue("@TrangThai", hd.TrangThai ?? "Còn hiệu lực");
            });
        }

        public void Update(HopDongLaoDong hd)
        {
            string query = @"
                UPDATE HopDongLaoDong SET
                    MaNV = @MaNV,
                    LoaiHopDong = @LoaiHopDong,
                    NgayKy = @NgayKy,
                    NgayHieuLuc = @NgayHieuLuc,
                    NgayKetThuc = @NgayKetThuc,
                    LuongCoBan = @LuongCoBan,
                    ThoiHanHD = @ThoiHanHD,
                    TrangThai = @TrangThai
                WHERE MaHopDong = @MaHopDong";
            ExecuteNonQueryWithTransaction(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@MaHopDong", hd.MaHopDong);
                cmd.Parameters.AddWithValue("@MaNV", hd.MaNV);
                cmd.Parameters.AddWithValue("@LoaiHopDong", hd.LoaiHopDong);
                cmd.Parameters.AddWithValue("@NgayKy", hd.NgayKy);
                cmd.Parameters.AddWithValue("@NgayHieuLuc", hd.NgayHieuLuc);
                cmd.Parameters.AddWithValue("@NgayKetThuc", hd.NgayKetThuc.HasValue ? (object)hd.NgayKetThuc.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@LuongCoBan", hd.LuongCoBan);
                cmd.Parameters.AddWithValue("@ThoiHanHD", hd.ThoiHanHD);
                cmd.Parameters.AddWithValue("@TrangThai", hd.TrangThai);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQueryWithTransaction("DELETE FROM HopDongLaoDong WHERE MaHopDong = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }

        public HopDongLaoDong GetByMaNV(string maNV)
        {
            var result = ExecuteQuery("SELECT * FROM HopDongLaoDong WHERE MaNV = @maNV",
                cmd => cmd.Parameters.AddWithValue("@maNV", maNV));
            return result.Count > 0 ? result[0] : null;
        }
    }
}
