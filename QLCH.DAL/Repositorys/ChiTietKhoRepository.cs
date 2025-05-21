using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class ChiTietKhoRepository
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

        private List<ChiTietKho> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<ChiTietKho>();
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
                            result.Add(new ChiTietKho
                            {
                                MaChiTietKho = Convert.ToInt32(reader["MaChiTietKho"]),
                                MaKho = Convert.ToInt32(reader["MaKho"]),
                                MaBienThe = Convert.ToInt32(reader["MaBienThe"]),
                                SoLuong = Convert.ToInt32(reader["SoLuong"]),
                                TrangThai = reader["TrangThai"].ToString()
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<ChiTietKho> GetAll() => ExecuteQuery("SELECT * FROM ChiTietKho");

        public ChiTietKho GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM ChiTietKho WHERE MaChiTietKho = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(ChiTietKho ct)
        {
            ExecuteNonQuery(@"INSERT INTO ChiTietKho (MaKho, MaBienThe, SoLuong, TrangThai)
                              VALUES (@MaKho, @MaBienThe, @SoLuong, @TrangThai)", cmd =>
            {
                cmd.Parameters.AddWithValue("@MaKho", ct.MaKho);
                cmd.Parameters.AddWithValue("@MaBienThe", ct.MaBienThe);
                cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                cmd.Parameters.AddWithValue("@TrangThai", (object)ct.TrangThai ?? "Còn hàng");
            });
        }

        public void Update(ChiTietKho ct)
        {
            ExecuteNonQuery(@"UPDATE ChiTietKho SET 
                                MaKho = @MaKho,
                                MaBienThe = @MaBienThe,
                                SoLuong = @SoLuong,
                                TrangThai = @TrangThai
                              WHERE MaChiTietKho = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", ct.MaChiTietKho);
                cmd.Parameters.AddWithValue("@MaKho", ct.MaKho);
                cmd.Parameters.AddWithValue("@MaBienThe", ct.MaBienThe);
                cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                cmd.Parameters.AddWithValue("@TrangThai", (object)ct.TrangThai ?? "Còn hàng");
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM ChiTietKho WHERE MaChiTietKho = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }

        public void TruTonKho(int maKho, int maBienThe, int soLuongXuat)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        var cmd = new SqlCommand("SELECT SoLuong FROM ChiTietKho WHERE MaKho = @MaKho AND MaBienThe = @MaBienThe", conn, trans);
                        cmd.Parameters.AddWithValue("@MaKho", maKho);
                        cmd.Parameters.AddWithValue("@MaBienThe", maBienThe);
                        object result = cmd.ExecuteScalar();

                        if (result == null)
                            throw new Exception("Không tìm thấy tồn kho.");

                        int soLuongHienTai = Convert.ToInt32(result);
                        if (soLuongHienTai < soLuongXuat)
                            throw new Exception("Không đủ hàng trong kho.");

                        int soLuongConLai = soLuongHienTai - soLuongXuat;

                        var updateCmd = new SqlCommand(@"
                    UPDATE ChiTietKho SET 
                        SoLuong = @SoLuongMoi, 
                        TrangThai = CASE WHEN @SoLuongMoi > 0 THEN N'Còn hàng' ELSE N'Hết hàng' END 
                    WHERE MaKho = @MaKho AND MaBienThe = @MaBienThe", conn, trans);

                        updateCmd.Parameters.AddWithValue("@SoLuongMoi", soLuongConLai);
                        updateCmd.Parameters.AddWithValue("@MaKho", maKho);
                        updateCmd.Parameters.AddWithValue("@MaBienThe", maBienThe);
                        updateCmd.ExecuteNonQuery();

                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

    }
}
