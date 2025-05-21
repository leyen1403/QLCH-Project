using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class ChiTietPhieuNhapRepository
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

        private List<ChiTietPhieuNhap> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<ChiTietPhieuNhap>();
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
                            result.Add(new ChiTietPhieuNhap
                            {
                                MaChiTiet = Convert.ToInt32(reader["MaChiTiet"]),
                                MaPhieuNhap = Convert.ToInt32(reader["MaPhieuNhap"]),
                                MaBienThe = Convert.ToInt32(reader["MaBienThe"]),
                                SoLuongNhap = Convert.ToInt32(reader["SoLuongNhap"]),
                                DonGiaNhap = Convert.ToDecimal(reader["DonGiaNhap"]),
                                ThanhTien = Convert.ToDecimal(reader["ThanhTien"]) // Computed column
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<ChiTietPhieuNhap> GetAll() => ExecuteQuery("SELECT * FROM ChiTietPhieuNhap");

        public ChiTietPhieuNhap GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM ChiTietPhieuNhap WHERE MaChiTiet = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(ChiTietPhieuNhap ct)
        {
            ExecuteNonQuery(@"INSERT INTO ChiTietPhieuNhap (MaPhieuNhap, MaBienThe, SoLuongNhap, DonGiaNhap)
                              VALUES (@MaPhieuNhap, @MaBienThe, @SoLuongNhap, @DonGiaNhap)", cmd =>
            {
                cmd.Parameters.AddWithValue("@MaPhieuNhap", ct.MaPhieuNhap);
                cmd.Parameters.AddWithValue("@MaBienThe", ct.MaBienThe);
                cmd.Parameters.AddWithValue("@SoLuongNhap", ct.SoLuongNhap);
                cmd.Parameters.AddWithValue("@DonGiaNhap", ct.DonGiaNhap);
            });
        }

        public void Update(ChiTietPhieuNhap ct)
        {
            ExecuteNonQuery(@"UPDATE ChiTietPhieuNhap SET 
                                MaPhieuNhap = @MaPhieuNhap,
                                MaBienThe = @MaBienThe,
                                SoLuongNhap = @SoLuongNhap,
                                DonGiaNhap = @DonGiaNhap
                              WHERE MaChiTiet = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", ct.MaChiTiet);
                cmd.Parameters.AddWithValue("@MaPhieuNhap", ct.MaPhieuNhap);
                cmd.Parameters.AddWithValue("@MaBienThe", ct.MaBienThe);
                cmd.Parameters.AddWithValue("@SoLuongNhap", ct.SoLuongNhap);
                cmd.Parameters.AddWithValue("@DonGiaNhap", ct.DonGiaNhap);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM ChiTietPhieuNhap WHERE MaChiTiet = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
