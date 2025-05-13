using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class KhachHangRepository
    {
        private readonly string _connectionString;
        public KhachHangRepository()
        {
            if (GlobalVariables.IsTestMode)
            {
                _connectionString = GlobalVariables.ConnectionString;
            }
            else
            {
                _connectionString = ConfigurationManager.ConnectionStrings["MyAppConnectionString"].ConnectionString;
            }

        }

        // Get All KhachHang
        public List<KhachHang> GetAll()
        {
            var list = new List<KhachHang>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM KhachHang", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    KhachHang khachHang = new KhachHang();
                    khachHang.MaKH = reader["MaKH"].ToString();
                    khachHang.TenKH = reader["TenKH"].ToString();
                    khachHang.DiaChi = reader["DiaChi"].ToString();
                    khachHang.SDT = reader["SDT"].ToString();
                    khachHang.Email = reader["Email"].ToString();
                    khachHang.ThoiGianTao = DateTime.Parse(reader["ThoiGianTao"].ToString());
                    khachHang.ThoiGianCapNhat = DateTime.Parse(reader["ThoiGianCapNhat"].ToString());
                    khachHang.TrangThai = (bool)reader["TrangThai"];
                    khachHang.DiemTichLuy = int.Parse(reader["DiemTichLuy"].ToString());
                    khachHang.TongChiTieu = decimal.Parse(reader["TongChiTieu"].ToString());
                    khachHang.LoaiKhachHang = (bool)reader["LoaiKhachHang"];
                    khachHang.PhanTramGiamGia = decimal.Parse(reader["PhanTramGiamGia"].ToString());
                    khachHang.CongNo = decimal.Parse(reader["CongNo"].ToString());
                    khachHang.SoLanGheTham = int.Parse(reader["SoLanGheTham"].ToString());
                    khachHang.SoLanMuaHang = int.Parse(reader["SoLanMuaHang"].ToString());
                    khachHang.GhiChu = reader["GhiChu"].ToString();
                    list.Add(khachHang);
                    Console.WriteLine($"Khách hàng: {khachHang.TenKH} - Mã KH: {khachHang.MaKH} - Số điện thoại: {khachHang.SDT}");
                }
                Console.WriteLine("Lấy danh sách khách hàng thành công!");
            }
            return list;
        }

        // Get By Id
        public KhachHang GetByID(string MaKH)
        {
            KhachHang khachHang = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM KhachHang WHERE MaKH = @MaKH", connection);
                command.Parameters.AddWithValue("@MaKH", MaKH);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    khachHang = new KhachHang
                    {
                        MaKH = reader["MaKH"].ToString(),
                        TenKH = reader["TenKH"].ToString(),
                        DiaChi = reader["DiaChi"].ToString(),
                        SDT = reader["SDT"].ToString(),
                        Email = reader["Email"].ToString(),
                        ThoiGianTao = DateTime.Parse(reader["ThoiGianTao"].ToString()),
                        ThoiGianCapNhat = DateTime.Parse(reader["ThoiGianCapNhat"].ToString()),
                        TrangThai = (bool)reader["TrangThai"],
                        DiemTichLuy = int.Parse(reader["DiemTichLuy"].ToString()),
                        TongChiTieu = decimal.Parse(reader["TongChiTieu"].ToString()),
                        LoaiKhachHang = (bool)reader["LoaiKhachHang"],
                        PhanTramGiamGia = decimal.Parse(reader["PhanTramGiamGia"].ToString()),
                        CongNo = decimal.Parse(reader["CongNo"].ToString()),
                        SoLanGheTham = int.Parse(reader["SoLanGheTham"].ToString()),
                        SoLanMuaHang = int.Parse(reader["SoLanMuaHang"].ToString()),
                        GhiChu = reader["GhiChu"].ToString()
                    };
                    Console.WriteLine("Lấy thông tin khách hàng thành công!");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy khách hàng với mã: " + MaKH);
                }
            }
            return khachHang;
        }

        // Add KhachHang
        public void Add(KhachHang khachHang)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    INSERT INTO KhachHang (
                        MaKH, TenKH, DiaChi, SDT, Email, ThoiGianTao, ThoiGianCapNhat,
                        TrangThai, DiemTichLuy, TongChiTieu, LoaiKhachHang,
                        PhanTramGiamGia, CongNo, SoLanGheTham, SoLanMuaHang, GhiChu
                    ) VALUES (
                        @MaKH, @TenKH, @DiaChi, @SDT, @Email, @ThoiGianTao, @ThoiGianCapNhat,
                        @TrangThai, @DiemTichLuy, @TongChiTieu, @LoaiKhachHang,
                        @PhanTramGiamGia, @CongNo, @SoLanGheTham, @SoLanMuaHang, @GhiChu
                    );
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Gọi hàm tái sử dụng để thêm parameter
                    AddParameters(command, khachHang);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected > 0 ? "Thêm khách hàng thành công!" : "Không thể thêm khách hàng.");
                }
            }
        }

        public void Update(KhachHang khachHang)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
            UPDATE KhachHang SET
                TenKH = @TenKH,
                DiaChi = @DiaChi,
                SDT = @SDT,
                Email = @Email,
                ThoiGianCapNhat = @ThoiGianCapNhat,
                TrangThai = @TrangThai,
                DiemTichLuy = @DiemTichLuy,
                TongChiTieu = @TongChiTieu,
                LoaiKhachHang = @LoaiKhachHang,
                PhanTramGiamGia = @PhanTramGiamGia,
                CongNo = @CongNo,
                SoLanGheTham = @SoLanGheTham,
                SoLanMuaHang = @SoLanMuaHang,
                GhiChu = @GhiChu
            WHERE MaKH = @MaKH;
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Tái sử dụng phương thức AddParameters để thêm toàn bộ tham số
                    AddParameters(command, khachHang);
                    connection.Open();

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected > 0 ? "Cập nhật khách hàng thành công!" : "Không thể cập nhật khách hàng.");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Lỗi cơ sở dữ liệu: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi không xác định: " + ex.Message);
                    }
                }
            }
        }


        // Delete KhachHang
        public void Delete(string maKH)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM KhachHang WHERE MaKH = @MaKH";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@MaKH", SqlDbType.NVarChar).Value = maKH;

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected > 0 ? "Xóa khách hàng thành công!" : "Không tìm thấy khách hàng để xóa.");
                }
            }
        }

        public string GetLastMaKH()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT TOP 1 MaKH FROM KhachHang ORDER BY MaKH DESC";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                var result = command.ExecuteScalar();
                Console.WriteLine(result != null ? "Lấy mã khách hàng cuối cùng thành công!" : "Không tìm thấy mã khách hàng.");
                return result != null ? result.ToString() : null;
            }
        }
        private void AddParameters(SqlCommand command, KhachHang khachHang)
        {
            command.Parameters.Add("@MaKH", SqlDbType.NVarChar).Value = khachHang.MaKH ?? (object)DBNull.Value;
            command.Parameters.Add("@TenKH", SqlDbType.NVarChar).Value = khachHang.TenKH ?? (object)DBNull.Value;
            command.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = khachHang.DiaChi ?? (object)DBNull.Value;
            command.Parameters.Add("@SDT", SqlDbType.NVarChar).Value = khachHang.SDT ?? (object)DBNull.Value;
            command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = khachHang.Email ?? (object)DBNull.Value;
            command.Parameters.Add("@ThoiGianTao", SqlDbType.DateTime).Value = khachHang.ThoiGianTao;
            command.Parameters.Add("@ThoiGianCapNhat", SqlDbType.DateTime).Value = khachHang.ThoiGianCapNhat;
            command.Parameters.Add("@TrangThai", SqlDbType.Bit).Value = khachHang.TrangThai;
            command.Parameters.Add("@DiemTichLuy", SqlDbType.Int).Value = khachHang.DiemTichLuy;
            command.Parameters.Add("@TongChiTieu", SqlDbType.Decimal).Value = khachHang.TongChiTieu;
            command.Parameters.Add("@LoaiKhachHang", SqlDbType.Bit).Value = khachHang.LoaiKhachHang;
            command.Parameters.Add("@PhanTramGiamGia", SqlDbType.Decimal).Value = khachHang.PhanTramGiamGia;
            command.Parameters.Add("@CongNo", SqlDbType.Decimal).Value = khachHang.CongNo;
            command.Parameters.Add("@SoLanGheTham", SqlDbType.Int).Value = khachHang.SoLanGheTham;
            command.Parameters.Add("@SoLanMuaHang", SqlDbType.Int).Value = khachHang.SoLanMuaHang;
            command.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = khachHang.GhiChu ?? (object)DBNull.Value;
        }
    }
}
