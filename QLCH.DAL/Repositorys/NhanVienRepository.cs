﻿// NhanVienRepository.cs
using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QLCH.DAL.Repositorys
{
    public class NhanVienRepository
    {
        private readonly string _connectionString;

        public NhanVienRepository()
        {
            _connectionString = GlobalVariables.ConnectionString;
        }

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

        private List<NhanVien> ExecuteQuery(string query, Action<SqlCommand> parameterMapper = null)
        {
            List<NhanVien> nhanViens = new List<NhanVien>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        parameterMapper?.Invoke(command);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                nhanViens.Add(MapNhanVien(reader));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi thực thi SQL: {ex.Message}");
                }
            }
            return nhanViens;
        }

        private NhanVien MapNhanVien(SqlDataReader reader)
        {
            return new NhanVien
            {
                MaNV = reader["MaNV"].ToString(),
                HoTen = reader["HoTen"].ToString(),
                NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                GioiTinh = reader["GioiTinh"].ToString(),
                CMND_CCCD = reader["CMND_CCCD"].ToString(),
                MaSoThue = reader["MaSoThue"].ToString(),
                SoDienThoai = reader["SoDienThoai"].ToString(),
                Email = reader["Email"].ToString(),
                DiaChi = reader["DiaChi"].ToString(),
                MaChucVu = Convert.ToInt32(reader["MaChucVu"]),
                MaPhongBan = Convert.ToInt32(reader["MaPhongBan"]),
                MaCuaHang = Convert.ToInt32(reader["MaCuaHang"]),
                LoaiHopDong = reader["LoaiHopDong"].ToString(),
                TrangThai = reader["TrangThai"].ToString(),
                NgayVaoLam = Convert.ToDateTime(reader["NgayVaoLam"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : (DateTime?)reader["UpdatedAt"]
            };
        }

        private void MapParameters(SqlCommand command, NhanVien nhanVien)
        {
            command.Parameters.AddWithValue("@HoTen", nhanVien.HoTen);
            command.Parameters.AddWithValue("@NgaySinh", nhanVien.NgaySinh);
            command.Parameters.AddWithValue("@GioiTinh", nhanVien.GioiTinh);
            command.Parameters.AddWithValue("@CMND_CCCD", nhanVien.CMND_CCCD);
            command.Parameters.AddWithValue("@MaSoThue", nhanVien.MaSoThue ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@SoDienThoai", nhanVien.SoDienThoai);
            command.Parameters.AddWithValue("@Email", nhanVien.Email);
            command.Parameters.AddWithValue("@DiaChi", nhanVien.DiaChi);
            command.Parameters.AddWithValue("@MaChucVu", nhanVien.MaChucVu);
            command.Parameters.AddWithValue("@MaPhongBan", nhanVien.MaPhongBan);
            command.Parameters.AddWithValue("@MaCuaHang", nhanVien.MaCuaHang);
            command.Parameters.AddWithValue("@LoaiHopDong", nhanVien.LoaiHopDong);
            command.Parameters.AddWithValue("@TrangThai", nhanVien.TrangThai);
            command.Parameters.AddWithValue("@NgayVaoLam", nhanVien.NgayVaoLam);
        }

        public List<NhanVien> GetAll() => ExecuteQuery("SELECT * FROM NhanVien");

        public NhanVien GetById(string id)
        {
            var result = ExecuteQuery("SELECT * FROM NhanVien WHERE MaNV = @MaNV",
                command => command.Parameters.AddWithValue("@MaNV", id));

            return result.Count > 0 ? result[0] : null;
        }

        public void Add(NhanVien nhanVien)
        {
            string query = @"
                INSERT INTO NhanVien 
                (MaNV, HoTen, NgaySinh, GioiTinh, CMND_CCCD, MaSoThue, SoDienThoai, Email, DiaChi, 
                 MaChucVu, MaPhongBan, MaCuaHang, LoaiHopDong, TrangThai, NgayVaoLam, CreatedAt) 
                VALUES 
                (@MaNV, @HoTen, @NgaySinh, @GioiTinh, @CMND_CCCD, @MaSoThue, @SoDienThoai, @Email, @DiaChi, 
                 @MaChucVu, @MaPhongBan, @MaCuaHang, @LoaiHopDong, @TrangThai, @NgayVaoLam, GETDATE())";

            ExecuteNonQueryWithTransaction(query, command =>
            {
                MapParameters(command, nhanVien);
                command.Parameters.AddWithValue("@MaNV", nhanVien.MaNV);
            }); 
        }

        public void Update(NhanVien nhanVien)
        {
            string query = @"
                UPDATE NhanVien 
                SET HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, CMND_CCCD = @CMND_CCCD, 
                    MaSoThue = @MaSoThue, SoDienThoai = @SoDienThoai, Email = @Email, DiaChi = @DiaChi, 
                    MaChucVu = @MaChucVu, MaPhongBan = @MaPhongBan, MaCuaHang = @MaCuaHang, 
                    LoaiHopDong = @LoaiHopDong, TrangThai = @TrangThai, NgayVaoLam = @NgayVaoLam, 
                    UpdatedAt = GETDATE()
                WHERE MaNV = @MaNV";

            ExecuteNonQueryWithTransaction(query, command =>
            {
                MapParameters(command, nhanVien);
                command.Parameters.AddWithValue("@MaNV", nhanVien.MaNV);
            });
        }

        public void Delete(string id)
        {
            string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
            ExecuteNonQueryWithTransaction(query, command => command.Parameters.AddWithValue("@MaNV", id));
        }

        public NhanVien GetLastNhanVien()
        {
            var result = ExecuteQuery("SELECT TOP 1 * FROM NhanVien ORDER BY CreatedAt DESC");
            return result.Count > 0 ? result[0] : null;
        }

        public bool Exists(string maNV)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT COUNT(*) FROM NhanVien WHERE MaNV = @MaNV", conn);
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public List<string> GetNhanVienChuaCoTaiKhoan()
        {
            var result = new List<string>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(@"
            SELECT MaNV FROM NhanVien
            WHERE MaNV NOT IN (SELECT MaNV FROM TaiKhoan)", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader["MaNV"].ToString());
                    }
                }
            }
            return result;
        }

    }
}
