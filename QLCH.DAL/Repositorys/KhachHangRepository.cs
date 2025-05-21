using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class KhachHangRepository
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

        private List<KhachHang> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<KhachHang>();
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
                            result.Add(new KhachHang
                            {
                                MaKhachHang = Convert.ToInt32(reader["MaKhachHang"]),
                                HoTen = reader["HoTen"].ToString(),
                                SoDienThoai = reader["SoDienThoai"].ToString(),
                                Email = reader["Email"]?.ToString(),
                                DiaChi = reader["DiaChi"]?.ToString(),
                                NgayDangKy = Convert.ToDateTime(reader["NgayDangKy"]),
                                DiemTichLuy = Convert.ToInt32(reader["DiemTichLuy"]),
                                LoaiKhachHang = reader["LoaiKhachHang"].ToString()
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<KhachHang> GetAll() => ExecuteQuery("SELECT * FROM KhachHang");

        public KhachHang GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM KhachHang WHERE MaKhachHang = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(KhachHang kh)
        {
            ExecuteNonQuery(@"INSERT INTO KhachHang (HoTen, SoDienThoai, Email, DiaChi, NgayDangKy, DiemTichLuy, LoaiKhachHang)
                              VALUES (@HoTen, @SoDienThoai, @Email, @DiaChi, @NgayDangKy, @DiemTichLuy, @LoaiKhachHang)", cmd =>
            {
                cmd.Parameters.AddWithValue("@HoTen", kh.HoTen);
                cmd.Parameters.AddWithValue("@SoDienThoai", kh.SoDienThoai);
                cmd.Parameters.AddWithValue("@Email", (object)kh.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DiaChi", (object)kh.DiaChi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayDangKy", kh.NgayDangKy == default ? DateTime.Now : kh.NgayDangKy);
                cmd.Parameters.AddWithValue("@DiemTichLuy", kh.DiemTichLuy);
                cmd.Parameters.AddWithValue("@LoaiKhachHang", kh.LoaiKhachHang ?? "Thành viên");
            });
        }

        public void Update(KhachHang kh)
        {
            ExecuteNonQuery(@"UPDATE KhachHang SET 
                                HoTen = @HoTen,
                                SoDienThoai = @SoDienThoai,
                                Email = @Email,
                                DiaChi = @DiaChi,
                                NgayDangKy = @NgayDangKy,
                                DiemTichLuy = @DiemTichLuy,
                                LoaiKhachHang = @LoaiKhachHang
                              WHERE MaKhachHang = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", kh.MaKhachHang);
                cmd.Parameters.AddWithValue("@HoTen", kh.HoTen);
                cmd.Parameters.AddWithValue("@SoDienThoai", kh.SoDienThoai);
                cmd.Parameters.AddWithValue("@Email", (object)kh.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DiaChi", (object)kh.DiaChi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayDangKy", kh.NgayDangKy);
                cmd.Parameters.AddWithValue("@DiemTichLuy", kh.DiemTichLuy);
                cmd.Parameters.AddWithValue("@LoaiKhachHang", kh.LoaiKhachHang ?? "Thành viên");
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM KhachHang WHERE MaKhachHang = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
