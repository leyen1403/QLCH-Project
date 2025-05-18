using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class NhanVienRepository
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
                        Console.WriteLine($"Lỗi khi thực thi SQL: {ex.Message}");
                    }
                }
            }
        }
        private List<NhanVien> ExecuteQuery(string query, Action<SqlCommand> parameterMapper = null)
        {
            List<NhanVien> nhanViens = new List<NhanVien>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
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
        public List<NhanVien> GetAll()
        {
            return ExecuteQuery("SELECT * FROM NhanVien");
        }
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
                (HoTen, NgaySinh, GioiTinh, CMND_CCCD, MaSoThue, SoDienThoai, Email, DiaChi, 
                 MaChucVu, MaPhongBan, MaCuaHang, LoaiHopDong, TrangThai, NgayVaoLam, CreatedAt) 
                VALUES 
                (@HoTen, @NgaySinh, @GioiTinh, @CMND_CCCD, @MaSoThue, @SoDienThoai, @Email, @DiaChi, 
                 @MaChucVu, @MaPhongBan, @MaCuaHang, @LoaiHopDong, @TrangThai, @NgayVaoLam, GETDATE())";

            ExecuteNonQueryWithTransaction(query, command =>
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
                command.Parameters.AddWithValue("@MaNV", nhanVien.MaNV);
                command.Parameters.AddWithValue("@HoTen", nhanVien.HoTen);
                command.Parameters.AddWithValue("@NgaySinh", nhanVien.NgaySinh);
                command.Parameters.AddWithValue("@GioiTinh", nhanVien.GioiTinh);
                command.Parameters.AddWithValue("@CMND_CCCD", nhanVien.CMND_CCCD);
                command.Parameters.AddWithValue("@MaSoThue", nhanVien.MaSoThue);
                command.Parameters.AddWithValue("@SoDienThoai", nhanVien.SoDienThoai);
                command.Parameters.AddWithValue("@Email", nhanVien.Email);
                command.Parameters.AddWithValue("@DiaChi", nhanVien.DiaChi);
            });
        }
        public void Delete(string id)
        {
            string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";

            ExecuteNonQueryWithTransaction(query, command =>
            {
                command.Parameters.AddWithValue("@MaNV", id);
            });
        }

        public NhanVien GetLastNhanVien()
        {
            var result = ExecuteQuery("SELECT TOP 1 * FROM NhanVien ORDER BY CreatedAt DESC");
            return result.Count > 0 ? result[0] : null;
        }
    }
}
